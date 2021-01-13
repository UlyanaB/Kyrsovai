using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Registrator
{
    public class Registrate
    {
        public const string prefix = @"http://localhost:8081/Processing/"; 

        private static volatile bool stpFlg = false;
        private static HttpListener listener = null;
        private static AdminDBEntities db = null;

        static void Main(string[] args)
        {
            Console.WriteLine("Starting ... ");
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("OS " + Environment.OSVersion.ToString() + " does not support HttpListener.");
                return;
            }
            db = new AdminDBEntities();
            ThreadPool.QueueUserWorkItem(RegListener);
            ThreadPool.QueueUserWorkItem(RemoveOldAddr);
            Console.WriteLine("To exit write Stop and press Enter");
            while(Console.ReadLine() != "Stop"){ }
            stpFlg = true;
            Console.WriteLine("Stopping ...");
            listener.Abort();

            Console.ReadLine();
        }

        private static void RemoveOldAddr(object state)
        {
            while(!stpFlg)
            {
                IList<tmpAdminDB> oldRequests = db.tmpAdminDB.Where(x => x.dt < DateTime.Now).ToList();
                foreach (tmpAdminDB oneTmpAdminDB in oldRequests)
                {
                    db.tmpAdminDB.Remove(oneTmpAdminDB);
                    Console.WriteLine("Request " + oneTmpAdminDB.guid_admin.ToString() + " expired");
                }
                db.SaveChanges();
                Thread.Sleep(1 * 1000);
            }
        }

        private static void RegListener(object state)
        {
            Console.WriteLine("Listener starting ... ");
            try
            {
                listener = new HttpListener();
                listener.Prefixes.Add(prefix);
                listener.Start(); 
                while (!stpFlg)
                {
                    HttpListenerContext context = listener.GetContext();
                    ThreadPool.QueueUserWorkItem(ProcessOneContext, context);
                }
            }
            catch(Exception ex)
            {
                stpFlg = true;
                Console.WriteLine("Exception in listener: " + ex.Message);
            }
            finally
            {
                if (listener != null)
                {
                    try
                    {
                        listener.Stop();
                        listener.Close();
                    }
                    catch (Exception) { }
                }
            }
            Console.WriteLine("Listener stopped");
        }

        private static void ProcessOneContext(object state)
        {
            HttpListenerContext context = null;
            HttpListenerRequest request = null;
            HttpListenerResponse response = null;
            try
            {
                context = state as HttpListenerContext;
                Console.WriteLine("Starting process context ... ");
                request = context.Request;
                NameValueCollection nameValueCollection = request.QueryString;
                string gs = request.QueryString.GetValues("id")[0]
                                   .Split(new string[] { @"/target=" }, StringSplitOptions.RemoveEmptyEntries)[0];
                Guid guid = Guid.Parse(gs);
                Console.WriteLine("context contain guid " + gs.ToString());
                tmpAdminDB oneTmpAdminDB = null;
                if ((oneTmpAdminDB = db.tmpAdminDB.FirstOrDefault(x => x.guid_admin == guid)) == null)
                {
                    Console.WriteLine("db does not contain guid " + gs.ToString());
                    return;
                }

                db.adminDB.Add(new adminDB() { name = oneTmpAdminDB.name, logina = oneTmpAdminDB.logina, passworda = oneTmpAdminDB.passworda, mail = oneTmpAdminDB.mail });
                db.tmpAdminDB.Remove(oneTmpAdminDB);
                db.SaveChanges();

                response = context.Response;
                string responseString = "<HTML><h2>Вы успешно зарегистрированы</h2></HTML>";
                byte[] buffer = Encoding.GetEncoding(1251).GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                using (Stream output = response.OutputStream){ output.Write(buffer, 0, buffer.Length); }
                db.logs.Add(new logs() { dt = DateTime.Now, msg = "Пользователь " + oneTmpAdminDB.name + " успешно зарегистрирован"});
                Console.WriteLine("request confirmed");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in context processing: " + ex.Message);
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
        }
    }
}
