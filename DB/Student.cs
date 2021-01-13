using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

using System.Windows.Forms;

namespace DB
{
    public partial class Student : Form
    {
        private string additionalHandling = "";
        long LimitRows;
        long OffSetRows = 0;
        EnumTabels Metod = EnumTabels.None;
        long PageNumber = 1;
        long NumberRows;
        long NotGo;
        long BlockForward;
        long OffSetRowsAll;
        long MaxRows;
        string SelectAll;
        string SelectName;
        string SelectSurname;
        string SelectPatronymic;

        /// <summary>
        /// конструктор формы
        /// </summary>
        public Student()
        {
            InitializeComponent();
        }

        #region Работа с отчетами и формами ввода
        /// <summary>
        /// датаГрид в режим просмотра
        /// </summary>
        private void viewOnly()
        {
            btnSave.Enabled = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
        }

        /// <summary>
        /// датаГрид в режим редактирования
        /// </summary>
        private void allowEdit()
        {
            btnSave.Enabled = true;
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.AllowUserToDeleteRows = true;
        }

        /// <summary>
        /// для всех кнопок по которым происходит только отображение данных
        /// </summary>
        /// <param name="sql"></param>
        private void all_select_button(string sql)
        {
            viewOnly();
            Program.mainForm.da = new NpgsqlDataAdapter(sql, Program.mainForm.con);
            Program.mainForm.ds.Reset();
            Program.mainForm.da.Fill(Program.mainForm.ds);
            Program.mainForm.dt = Program.mainForm.ds.Tables[0];
            dataGridView1.DataSource = Program.mainForm.dt;
        }
        private void all_select_button(NpgsqlCommand npgsqlCommand)
        {
            viewOnly();

            Program.mainForm.da = new NpgsqlDataAdapter(npgsqlCommand);

            NpgsqlCommandBuilder npgsqlCommandBuilder = new NpgsqlCommandBuilder(Program.mainForm.da);

            //Program.mainForm.da = new NpgsqlDataAdapter(sql, Program.mainForm.con);
            Program.mainForm.ds.Reset();
            Program.mainForm.da.Fill(Program.mainForm.ds);
            Program.mainForm.dt = Program.mainForm.ds.Tables[0];
            dataGridView1.DataSource = Program.mainForm.dt;
        }

        /// <summary>
        /// для всех кнопок по которым происходит только отображение данных 
        /// и один параметр
        /// </summary>
        /// <param name="sql"></param>
        private void all_select_button(string sql, object param1)
        {
            viewOnly();
            Program.mainForm.da = new NpgsqlDataAdapter(sql, Program.mainForm.con);
            Program.mainForm.da.SelectCommand.Parameters.AddWithValue("param1", param1);
            Program.mainForm.ds.Reset();
            Program.mainForm.da.Fill(Program.mainForm.ds);
            Program.mainForm.dt = Program.mainForm.ds.Tables[0];
            dataGridView1.DataSource = Program.mainForm.dt;
        }

        /// <summary>
        /// для всех кнопок по которым происходит только отображение данных 
        /// и один параметр
        /// </summary>
        /// <param name="sql"></param>
        private void all_select_button(string sql, object param1, object param2)
        {
            viewOnly();
            Program.mainForm.da = new NpgsqlDataAdapter(sql, Program.mainForm.con);
            Program.mainForm.da.SelectCommand.Parameters.AddWithValue("param1", param1);
            Program.mainForm.da.SelectCommand.Parameters.AddWithValue("param2", param2);
            Program.mainForm.ds.Reset();
            Program.mainForm.da.Fill(Program.mainForm.ds);
            Program.mainForm.dt = Program.mainForm.ds.Tables[0];
            dataGridView1.DataSource = Program.mainForm.dt;
        }

        /// <summary>
        /// для всех кнопок по которым допускается изменение даннх
        /// </summary>
        /// <param name="select"></param>
        private void all_update_button(string select)
        {
            allowEdit();
            Program.mainForm.da = new NpgsqlDataAdapter(select, Program.mainForm.con);

            NpgsqlCommandBuilder npgsqlCommandBuilder = new NpgsqlCommandBuilder(Program.mainForm.da);
            Program.mainForm.da.InsertCommand = npgsqlCommandBuilder.GetInsertCommand();
            Program.mainForm.da.UpdateCommand = npgsqlCommandBuilder.GetUpdateCommand();
            Program.mainForm.da.DeleteCommand = npgsqlCommandBuilder.GetDeleteCommand();
            Program.mainForm.ds.Reset();
            Program.mainForm.da.Fill(Program.mainForm.ds);
            Program.mainForm.dt = Program.mainForm.ds.Tables[0];
            dataGridView1.DataSource = Program.mainForm.dt;
            dataGridView1.Columns[0].ReadOnly = false;
            dataGridView1.Columns[1].ReadOnly = false;

        }
        /// <summary>
        /// для всех кнопок по которым допускается изменение даннх
        /// </summary>
        /// <param name="select"></param>
        private void all_update_button(NpgsqlCommand npgsqlCommand)
        {
            allowEdit();
            Program.mainForm.da = new NpgsqlDataAdapter(npgsqlCommand);
            Program.mainForm.da.RowUpdated += Da_RowUpdated;
            NpgsqlCommandBuilder npgsqlCommandBuilder = new NpgsqlCommandBuilder(Program.mainForm.da);
            Program.mainForm.da.InsertCommand = npgsqlCommandBuilder.GetInsertCommand();
            Program.mainForm.da.UpdateCommand = npgsqlCommandBuilder.GetUpdateCommand();
            Program.mainForm.da.DeleteCommand = npgsqlCommandBuilder.GetDeleteCommand();
            Program.mainForm.ds.Reset();
            Program.mainForm.da.Fill(Program.mainForm.ds);
            Program.mainForm.dt = Program.mainForm.ds.Tables[0];
            dataGridView1.DataSource = Program.mainForm.dt;
            dataGridView1.Columns[0].ReadOnly = false;
            dataGridView1.Columns[1].ReadOnly = false;

        }

        private string ToString(object[] obj)
        {
            string res = null;
            for(int i = 1; i < obj.Length; i++)
            {
                res += ", " + obj[i].ToString();
            }
            return res;
        }

        private void Da_RowUpdated(object sender, NpgsqlRowUpdatedEventArgs e)
        {
            switch(e.StatementType)
            {
                case System.Data.StatementType.Insert:
                    Program.mainForm.toLog.Add(Program.mainForm.IdAdmin, EnumSeverity.Info, "Добавленно в " 
                        + this.Metod.ToString() + " " + ToString(e.Row.ItemArray));

                    break;
                case System.Data.StatementType.Delete:
                    Program.mainForm.toLog.Add(Program.mainForm.IdAdmin, EnumSeverity.Info, "Удалено из "
                        + this.Metod.ToString());

                    break;
                case System.Data.StatementType.Update:
                    Program.mainForm.toLog.Add(Program.mainForm.IdAdmin, EnumSeverity.Info, "Изменено в "
                        + this.Metod.ToString() + " на строке " + e.Row.ItemArray[0].ToString() + " на " + ToString(e.Row.ItemArray));

                    break;
            }
            
        }

        private void all_update_button_OnlyClass(NpgsqlCommand npgsqlCommand)
        {
            allowEdit();
            Program.mainForm.da = new NpgsqlDataAdapter(npgsqlCommand);

            NpgsqlCommandBuilder npgsqlCommandBuilder = new NpgsqlCommandBuilder(Program.mainForm.da);
            Program.mainForm.da.InsertCommand = npgsqlCommandBuilder.GetInsertCommand();
            IList<NpgsqlParameter> prms = new List<NpgsqlParameter>();
            foreach (NpgsqlParameter oneParam in Program.mainForm.da.InsertCommand.Parameters)
            {
                prms.Add(oneParam.Clone());
            }
            Program.mainForm.da.InsertCommand = new NpgsqlCommand("INSERT INTO \"school\".\"public\".\"class0\"(\"class_number\", \"class_flag\") VALUES(@p1, 'Y')", Program.mainForm.con);
            Program.mainForm.da.InsertCommand.Parameters.AddRange(prms.ToArray());
            Program.mainForm.da.UpdateCommand = npgsqlCommandBuilder.GetUpdateCommand();
            Program.mainForm.da.DeleteCommand = npgsqlCommandBuilder.GetDeleteCommand();
            prms.Clear();
            foreach (NpgsqlParameter oneParam in Program.mainForm.da.UpdateCommand.Parameters)
            {
                prms.Add(oneParam.Clone());
            }
            Program.mainForm.da.DeleteCommand = new NpgsqlCommand("UPDATE \"school\".\"public\".\"class0\" SET \"class_flag\" = 'N' WHERE ((\"id_class\" = @p2) AND (\"class_number\" = @p3))", Program.mainForm.con);
            Program.mainForm.da.DeleteCommand.Parameters.AddRange(prms.ToArray());
            Program.mainForm.ds.Reset();
            Program.mainForm.da.Fill(Program.mainForm.ds);
            Program.mainForm.dt = Program.mainForm.ds.Tables[0];
            dataGridView1.DataSource = Program.mainForm.dt;
            dataGridView1.Columns[0].ReadOnly = false;
            dataGridView1.Columns[1].ReadOnly = false;

        }

        private void PrepareGrid()
        {
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.ReadOnly = false;
                column.Visible = true;
            }
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
        }


        #endregion Работа с отчетами и формами ввода

        #region Методы для работы с отчетами
        /// <summary>
        /// количество заказов по сотрудникам
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOrderQuantity_Click(object sender, EventArgs e)
        {
            PageNumber = 1;

            ComBoxAll.Text = "";
            ComBoxName.Text = "";
            ComBoxSurname.Text = "";
            ComBoxPatronymic.Text = "";

            BtnConnectFilter.Enabled = true;
            BtnBack.Enabled = false;
            BtnForward.Enabled = false;

            ComBoxAll.Enabled = true;
            ComBoxName.Enabled = true;
            ComBoxSurname.Enabled = true;
            ComBoxPatronymic.Enabled = true;

            Metod = EnumTabels.OrderStudentChoice;
            OffSetRows = 0;
            NumberRows = int.Parse(ComBoxLimitRows.SelectedItem.ToString());
            BtnOrderStudentChoice();
            DinamFilterValueStudentObject();
        }

        private void BtnOrderStudentChoice()
        {
            LabPageNumber.Text = "Номер страницы: " + PageNumber;

            string sql = "SELECT chs.id_choicestudent, s.nams, s.secondnames, s.middlenames, ob.title " +
                "FROM choicestudent chs, student s, object0 ob " +
                "WHERE s.id_student=chs.id_student and ob.id_object=chs.id_object LIMIT @limit OFFSET @offset;";
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand(sql, Program.mainForm.con);
            LimitRows = int.Parse(ComBoxLimitRows.SelectedItem.ToString());
            npgsqlCommand.Parameters.AddWithValue("@limit", LimitRows);
            npgsqlCommand.Parameters.AddWithValue("@offset", OffSetRows);
            all_select_button(npgsqlCommand);
            //additionalHandling = "class0";
            if (OffSetRows >= 0)
            {
                BtnForward.Enabled = true;
                BtnForwardAll.Enabled = true;
                BtnBackAll.Enabled = true;
            }
            BlockBtnForward("choicestudent chs, student s, object0 ob WHERE s.id_student=chs.id_student and ob.id_object=chs.id_object");
        }

        /// <summary>
        /// доход по сотрудникам
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWorkerProfit_Click(object sender, EventArgs e)
        {
            //string sql = "select worker.id_worker, name \"имя\", second_name \"фамилия\", procent, " +
                        //"       (sum(salary_a)*procent)+ salary as pay " +
                        //"   from worker, older, activies " +
                        //"   where   older.id_worker = worker.id_worker " +
                        //"       and older.id_activity = activies.id_activity " +
                        //"   group by worker.id_worker;";
            //all_select_button(sql);
        }

        /// <summary>
        /// доход компании по сотрудникам
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFirmProfit_Click(object sender, EventArgs e)
        {
            //string sql = "select worker.id_worker, name, second_name, procent, " +
                            //"       sum(salary_a) - (sum(salary_a)*procent) as pay_org " +
                            //"   from worker, older, activies " +
                            //"   where   older.id_worker = worker.id_worker " +
                            //"       and older.id_activity = activies.id_activity " +
                            //"   group by worker.id_worker " +
                            //"   order by second_name, name;";
            //all_select_button(sql);
        }

        /// <summary>
        /// Заказы сотрудников
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOrderByWorker_Click(object sender, EventArgs e)
        {
            string sql = "select worker.second_name \"Фамилия\", worker.name \"Имя\", worker.procent \"%\", " +
                         "       activies.name_a \"наименование работы\", activies.salary_a \"стоимость работы\", " +
                         "       older.date  \"дата\"" +
                         "   from worker " +
                         "   left join older on worker.id_worker = older.id_worker " +
                         "   left join activies on older.id_activity = activies.id_activity " +
                         "   where worker.second_name = @param1 " +
                         "   order by second_name, name;";
            string param1 = tbParam1.Text;
            all_select_button(sql, param1);
        }

        /// <summary>
        /// Заказы сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWorkersOrder_Click(object sender, EventArgs e)
        {
            PageNumber = 1;

            ComBoxAll.Text = "";
            ComBoxName.Text = "";
            ComBoxSurname.Text = "";
            ComBoxPatronymic.Text = "";

            BtnConnectFilter.Enabled = true;
            BtnBack.Enabled = false;
            BtnForward.Enabled = false;

            ComBoxAll.Enabled = true;
            ComBoxName.Enabled = true;
            ComBoxSurname.Enabled = true;
            ComBoxPatronymic.Enabled = true;

            Metod = EnumTabels.OrderStudent;
            OffSetRows = 0;
            NumberRows = int.Parse(ComBoxLimitRows.SelectedItem.ToString());
            BtnOrderStudent();
            DinamFilterValueStudent();
        }

        private void BtnOrderStudent()
        {
            LabPageNumber.Text = "Номер страницы: " + PageNumber;

            string sql = "SELECT s.id_student, c0.class_number, s.nams, s.secondnames, s.middlenames, s.logins, s.passwords " +
                        "FROM student s join class0 c0 on s.id_class = c0.id_class ORDER BY c0.class_number " +
                        "LIMIT @limit OFFSET @offset; ";
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand(sql, Program.mainForm.con);
            LimitRows = int.Parse(ComBoxLimitRows.SelectedItem.ToString());
            npgsqlCommand.Parameters.AddWithValue("@limit", LimitRows);
            npgsqlCommand.Parameters.AddWithValue("@offset", OffSetRows);
            all_select_button(npgsqlCommand);
            //additionalHandling = "class0";
            if (OffSetRows >= 0)
            {
                BtnForward.Enabled = true;
                BtnForwardAll.Enabled = true;
                BtnBackAll.Enabled = true;
            }
            BlockBtnForward("student s join class0 c0 on s.id_class = c0.id_class");
        }

        private void BtnConnectFilter_Click(object sender, EventArgs e)
        {
            switch (Metod)
            {
                case EnumTabels.OrderStudent:
                    ConnectFilterStudent();

                    break;
                case EnumTabels.OrderStudentChoice:
                    ConnectFilterObjectStudent();

                    break;
                case EnumTabels.ConnectFilterStudent:
                    ConnectFilterStudent();

                    break;
                case EnumTabels.Student:
                    ConnectFilterStudent();

                    break;
                case EnumTabels.ConnectFilterObjectStudent:
                    ConnectFilterObjectStudent();

                break;
                case EnumTabels.StudentChoice:
                    ConnectFilterObjectStudent();

                    break;


                default:
                    throw new Exception("Нету обработки фильтр: " + Metod.ToString());
            }
        }

        private void ConnectFilterStudent()

        {
            Metod = EnumTabels.ConnectFilterStudent;
            LabPageNumber.Text = "Номер страницы: " + PageNumber;
            try
            {
                PrepareGrid();

                string sqlPattern = "SELECT {0} " +
                             "   FROM student s join class0 c0 on s.id_class = c0.id_class" +
                             "   WHERE (c0.class_number = @class0 OR @class0 = '')" +
                             "   AND (s.nams = @names OR @names = '')" +
                             "   AND (s.secondnames = @surname  OR @surname = '')" +
                             "   AND (s.middlenames = @patronymic OR @patronymic = '')" +
                             "   {1};";


                string sql = string.Format(sqlPattern, "s.id_student, c0.class_number, s.nams, s.secondnames, s.middlenames, s.logins, s.passwords", "LIMIT @limit OFFSET @offset");
                string sqlCount = string.Format(sqlPattern, "count(*)", "");

                NpgsqlCommand npgsqlCommand = new NpgsqlCommand(sql, Program.mainForm.con);
                LimitRows = int.Parse(ComBoxLimitRows.SelectedItem.ToString());
                SelectAll = ComBoxAll.SelectedItem == null ? "" : ComBoxAll.SelectedItem.ToString();
                SelectName = ComBoxName.SelectedItem == null ? "" : ComBoxName.SelectedItem.ToString();
                SelectSurname = ComBoxSurname.SelectedItem == null ? "" : ComBoxSurname.SelectedItem.ToString();
                SelectPatronymic = ComBoxPatronymic.SelectedItem == null ? "" : ComBoxPatronymic.SelectedItem.ToString();

                npgsqlCommand.Parameters.Add("@class0", NpgsqlTypes.NpgsqlDbType.Text, 20);
                npgsqlCommand.Parameters["@class0"].Value = SelectAll;

                npgsqlCommand.Parameters.Add("@names", NpgsqlTypes.NpgsqlDbType.Text, 20);
                npgsqlCommand.Parameters["@names"].Value = SelectName;

                npgsqlCommand.Parameters.Add("@surname", NpgsqlTypes.NpgsqlDbType.Text, 20);
                npgsqlCommand.Parameters["@surname"].Value = SelectSurname;

                npgsqlCommand.Parameters.Add("@patronymic", NpgsqlTypes.NpgsqlDbType.Text, 20);
                npgsqlCommand.Parameters["@patronymic"].Value = SelectPatronymic;
                npgsqlCommand.Parameters.AddWithValue("@limit", LimitRows);
                npgsqlCommand.Parameters.AddWithValue("@offset", OffSetRows);
                NpgsqlCommand npgsqlCommandConnect = new NpgsqlCommand(sqlCount, Program.mainForm.con);

                npgsqlCommandConnect.Parameters.AddRange(new NpgsqlParameter[] { npgsqlCommand.Parameters[0].Clone(),
                                                            npgsqlCommand.Parameters[1].Clone(), npgsqlCommand.Parameters[2].Clone(),
                                                            npgsqlCommand.Parameters[3].Clone() });
                all_select_button(npgsqlCommand);

                if (OffSetRows >= 0)
                {
                    BtnForward.Enabled = true;
                    BtnForwardAll.Enabled = true;
                    BtnBackAll.Enabled = true;
                }
                BlockBtnForwardConnect(npgsqlCommandConnect);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void ConnectFilterObjectStudent()
        {
            Metod = EnumTabels.ConnectFilterObjectStudent;
            LabPageNumber.Text = "Номер страницы: " + PageNumber;
            try
            {
                PrepareGrid();

                string sqlPattern = "SELECT {0} " +
                             "   FROM choicestudent chs, student s, object0 ob" +
                             "   WHERE s.id_student=chs.id_student AND ob.id_object=chs.id_object " +
                             "   AND (ob.title = @title OR @title = '')" +
                             "   AND (s.nams = @names OR @names = '')" +
                             "   AND (s.secondnames = @surname  OR @surname = '')" +
                             "   AND (s.middlenames = @patronymic OR @patronymic = '')" +
                             "   {1};";


                string sql = string.Format(sqlPattern, "chs.id_choicestudent, s.nams, s.secondnames, s.middlenames, ob.title", "LIMIT @limit OFFSET @offset");
                string sqlCount = string.Format(sqlPattern, "count(*)", "");

                NpgsqlCommand npgsqlCommand = new NpgsqlCommand(sql, Program.mainForm.con);
                LimitRows = int.Parse(ComBoxLimitRows.SelectedItem.ToString());
                SelectAll = ComBoxAll.SelectedItem == null ? "" : ComBoxAll.SelectedItem.ToString();
                SelectName = ComBoxName.SelectedItem == null ? "" : ComBoxName.SelectedItem.ToString();
                SelectSurname = ComBoxSurname.SelectedItem == null ? "" : ComBoxSurname.SelectedItem.ToString();
                SelectPatronymic = ComBoxPatronymic.SelectedItem == null ? "" : ComBoxPatronymic.SelectedItem.ToString();

                npgsqlCommand.Parameters.Add("@title", NpgsqlTypes.NpgsqlDbType.Text, 20);
                npgsqlCommand.Parameters["@title"].Value = SelectAll;

                npgsqlCommand.Parameters.Add("@names", NpgsqlTypes.NpgsqlDbType.Text, 20);
                npgsqlCommand.Parameters["@names"].Value = SelectName;

                npgsqlCommand.Parameters.Add("@surname", NpgsqlTypes.NpgsqlDbType.Text, 20);
                npgsqlCommand.Parameters["@surname"].Value = SelectSurname;

                npgsqlCommand.Parameters.Add("@patronymic", NpgsqlTypes.NpgsqlDbType.Text, 20);
                npgsqlCommand.Parameters["@patronymic"].Value = SelectPatronymic;
                npgsqlCommand.Parameters.AddWithValue("@limit", LimitRows);
                npgsqlCommand.Parameters.AddWithValue("@offset", OffSetRows);
                NpgsqlCommand npgsqlCommandConnect = new NpgsqlCommand(sqlCount, Program.mainForm.con);

                npgsqlCommandConnect.Parameters.AddRange(new NpgsqlParameter[] { npgsqlCommand.Parameters[0].Clone(),
                                                            npgsqlCommand.Parameters[1].Clone(), npgsqlCommand.Parameters[2].Clone(),
                                                            npgsqlCommand.Parameters[3].Clone() });
                all_select_button(npgsqlCommand);

                if (OffSetRows >= 0)
                {
                    BtnForward.Enabled = true;
                    BtnForwardAll.Enabled = true;
                    BtnBackAll.Enabled = true;
                }
                BlockBtnForwardConnect(npgsqlCommandConnect);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        #region фильтр обычный
        private void FilterComBoxName(string selectName)
        {
            ComBoxName.Items.Clear();
            NpgsqlDataReader npgsqlDataReader = null;
            try
            {
                npgsqlDataReader = new NpgsqlCommand(selectName, Program.mainForm.con).ExecuteReader();
                while (npgsqlDataReader.Read())
                {
                    ComBoxName.Items.Add(npgsqlDataReader.GetString(0));
                }
            }
            finally
            {
                npgsqlDataReader.Close();
            }
        }

        private void FilterComBoxAll(string selectSql)
        {
            ComBoxAll.Items.Clear();
            NpgsqlDataReader npgsqlDataReader = null;
            try
            {
                npgsqlDataReader = new NpgsqlCommand(selectSql, Program.mainForm.con).ExecuteReader();
                while (npgsqlDataReader.Read())
                {
                    ComBoxAll.Items.Add(npgsqlDataReader.GetString(0));
                }
            }
            finally
            {
                npgsqlDataReader.Close();
            }
        }

        private void FilterConBoxSurname(string selectSurname)
        {
            ComBoxSurname.Items.Clear();
            NpgsqlDataReader npgsqlDataReader = null;
            try
            {
                npgsqlDataReader = new NpgsqlCommand(selectSurname, Program.mainForm.con).ExecuteReader();
                while (npgsqlDataReader.Read())
                {
                    ComBoxSurname.Items.Add(npgsqlDataReader.GetString(0));
                }
            }
            finally
            {
                npgsqlDataReader.Close();
            }
        }

        private void FilterComBoxPatronymic(string selectPatronymic)
        {
            ComBoxPatronymic.Items.Clear();
            NpgsqlDataReader npgsqlDataReader = null;
            try
            {
                npgsqlDataReader = new NpgsqlCommand(selectPatronymic, Program.mainForm.con).ExecuteReader();
                while (npgsqlDataReader.Read())
                {
                    ComBoxPatronymic.Items.Add(npgsqlDataReader.GetString(0));
                }
            }
            finally
            {
                npgsqlDataReader.Close();
            }
        }

        #endregion

        private long getCountConnect(NpgsqlCommand npgsqlCommand)
        {
            return (long)(npgsqlCommand.ExecuteScalar());
        }


        private void BlockBtnForwardConnect(NpgsqlCommand npgsqlCommand)
        {

            BlockForward = getCountConnect(npgsqlCommand);
            if (BlockForward <= NumberRows)
            {
                BtnForward.Enabled = false;
            }
        }

        /// <summary>
        /// количество заказов на дату
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOrderByDate_Click(object sender, EventArgs e)
        {
            string sql = "select count(*) \"заказов\"" +
                         "   from older " +
                         "   where older.date = @param1 ";
            DateTime dt = DateTime.Parse(tbParam1.Text);
            //int i = int.Parse(tbParam1.Text);
            all_select_button(sql, dt);
        }
        #endregion Методы для работы с отчетами

        #region Методы для работы с формами ввода данных
        /// <summary>
        /// редактирование списка классов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClass_Click(object sender, EventArgs e)
        {
            PageNumber = 1;

            ComBoxAll.Text = "";
            ComBoxName.Text = "";
            ComBoxSurname.Text = "";
            ComBoxPatronymic.Text = "";

            BtnConnectFilter.Enabled = false;
            BtnBack.Enabled = false;
            BtnForward.Enabled = false;

            Metod = EnumTabels.Class;
            OffSetRows = 0;
            NumberRows = int.Parse(ComBoxLimitRows.SelectedItem.ToString());
            BtnClass();
        }

        private void BtnClass()
        {
            //BlockBtnForward();
            LabPageNumber.Text = "Номер страницы: " + PageNumber;
            //NumberRows = int.Parse(ComBoxLimitRows.SelectedItem.ToString())
            try
            {
                PrepareGrid();
                string select = "SELECT cl.id_class, cl.class_number FROM class0 cl WHERE cl.class_flag = 'Y' ORDER BY class_number LIMIT @limit OFFSET @offset;";
                //string select = "SELECT cl.id_class, cl.class_number, cl.class_flag FROM class0 cl ORDER BY class_number LIMIT @limit OFFSET @offset;";
                NpgsqlCommand npgsqlCommand = new NpgsqlCommand(select, Program.mainForm.con);
                LimitRows = int.Parse(ComBoxLimitRows.SelectedItem.ToString());
                npgsqlCommand.Parameters.AddWithValue("@limit", LimitRows);
                npgsqlCommand.Parameters.AddWithValue("@offset", OffSetRows);
                all_update_button_OnlyClass(npgsqlCommand);
                dataGridView1.Columns[0].ReadOnly = true;
                dataGridView1.Columns[0].Visible = false;
                additionalHandling = "class0";
                if (OffSetRows >= 0)
                {
                    BtnForward.Enabled = true;
                    BtnForwardAll.Enabled = true;
                    BtnBackAll.Enabled = true;
                }
                BlockBtnForward("class0");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        /// <summary>
        /// редактирование списка предметов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnObject_Click(object sender, EventArgs e)
        {
            PageNumber = 1;

            ComBoxAll.Text = "";
            ComBoxName.Text = "";
            ComBoxSurname.Text = "";
            ComBoxPatronymic.Text = "";

            BtnConnectFilter.Enabled = false;
            BtnBack.Enabled = false;
            BtnForward.Enabled = false;

            Metod = EnumTabels.Object0;
            OffSetRows = 0;
            NumberRows = int.Parse(ComBoxLimitRows.SelectedItem.ToString());
            BtnObject();
        }

        private void BtnObject()
        {
            LabPageNumber.Text = "Номер страницы: " + PageNumber;

            try
            {
                PrepareGrid();
                string select = "SELECT ob.id_object, ob.title FROM object0 ob LIMIT @limit OFFSET @offset;";
                NpgsqlCommand npgsqlCommand = new NpgsqlCommand(select, Program.mainForm.con);
                LimitRows = int.Parse(ComBoxLimitRows.SelectedItem.ToString());
                npgsqlCommand.Parameters.AddWithValue("@limit", LimitRows);
                npgsqlCommand.Parameters.AddWithValue("@offset", OffSetRows);
                all_update_button(npgsqlCommand);
                dataGridView1.Columns[0].ReadOnly = true;
                dataGridView1.Columns[0].Visible = false;
                additionalHandling = "object0";
                if (OffSetRows >= 0)
                {
                    BtnForward.Enabled = true;
                    BtnForwardAll.Enabled = true;
                    BtnBackAll.Enabled = true;
                }
                BlockBtnForward("object0");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        /// <summary>
        /// редактирование списка учащихся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStudents_Click(object sender, EventArgs e)
        {
            PageNumber = 1;

            ComBoxAll.Text = "";
            ComBoxName.Text = "";
            ComBoxSurname.Text = "";
            ComBoxPatronymic.Text = "";

            BtnConnectFilter.Enabled = true;
            BtnBack.Enabled = false;
            BtnForward.Enabled = false;

            ComBoxAll.Enabled = true;
            ComBoxName.Enabled = true;
            ComBoxSurname.Enabled = true;
            ComBoxPatronymic.Enabled = true;

            Metod = EnumTabels.Student;
            OffSetRows = 0;
            NumberRows = int.Parse(ComBoxLimitRows.SelectedItem.ToString());
            BtnStudent();
            DinamFilterValueStudent();
        }

        private void BtnStudent()
        {
            LabPageNumber.Text = "Номер страницы: " + PageNumber;
            try
            {
                PrepareGrid();
                string select = "SELECT s.id_student,                                                                                   " +
                                "       (select c.class_number from class0 c where c.id_class = s.id_class) class_number,               " +
                                "       s.nams, s.secondnames, s.middlenames, s.logins, s.passwords                                     " +
                                "   FROM    student s LIMIT @limit OFFSET @offset";
                NpgsqlCommand npgsqlCommand = new NpgsqlCommand(select, Program.mainForm.con);
                LimitRows = int.Parse(ComBoxLimitRows.SelectedItem.ToString());
                npgsqlCommand.Parameters.AddWithValue("@limit", LimitRows);
                npgsqlCommand.Parameters.AddWithValue("@offset", OffSetRows);
                all_update_button(npgsqlCommand);
                dataGridView1.Columns[0].ReadOnly = true;
                dataGridView1.Columns[0].Visible = false;
                additionalHandling = "student";
                if (OffSetRows >= 0)
                {
                    BtnForward.Enabled = true;
                    BtnForwardAll.Enabled = true;
                    BtnBackAll.Enabled = true;
                }
                BlockBtnForward("student");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        /// <summary>
        /// записать в БД то что отредактировали
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Program.mainForm.da.Update(Program.mainForm.ds);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            switch (Metod)
            {
                case EnumTabels.Student:
                    DinamFilterValueStudent();

                    break;
                case EnumTabels.StudentChoice:
                    DinamFilterValueStudentObject();

                    break;
                case EnumTabels.OrderStudentChoice:
                    DinamFilterValueStudentObject();

                    break;
                case EnumTabels.OrderStudent:
                    DinamFilterValueStudent();

                    break;
                case EnumTabels.ConnectFilterStudent:
                    DinamFilterValueStudent();

                    break;
                case EnumTabels.ConnectFilterObjectStudent:
                    DinamFilterValueStudentObject();

                    break;

                default:
                    throw new Exception("Нету фильтра " + Metod.ToString());
            }

        }

        /// <summary>
        /// редактирование 'выбора учащегося'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStudentChoice_Click(object sender, EventArgs e)
        {
            PageNumber = 1;

            ComBoxAll.Text = "";
            ComBoxName.Text = "";
            ComBoxSurname.Text = "";
            ComBoxPatronymic.Text = "";

            BtnConnectFilter.Enabled = true;
            BtnBack.Enabled = false;
            BtnForward.Enabled = false;

            ComBoxAll.Enabled = true;
            ComBoxName.Enabled = true;
            ComBoxSurname.Enabled = true;
            ComBoxPatronymic.Enabled = true;

            Metod = EnumTabels.StudentChoice;
            OffSetRows = 0;
            NumberRows = int.Parse(ComBoxLimitRows.SelectedItem.ToString());
            BtnStudentChoice();
            DinamFilterValueStudentObject();
            
        }

        private void BtnStudentChoice()
        {
            LabPageNumber.Text = "Номер страницы: " + PageNumber;

            try
            {
                PrepareGrid();
                string select = "SELECT chs.id_choicestudent,                                                               " +
                                "       s.id_student,                                                                       " +
                                "       (select c.class_number from class0 c where c.id_class = s.id_class) class_number,   " +
                                "       s.nams, s.secondnames, s.middlenames,                                               " +
                                "       chs.id_object, ob.title                                                             " +
                                "   FROM choicestudent chs                                                                  " +
                                "   JOIN student s on chs.id_student = s.id_student                                         " +
                                "   JOIN object0 ob on chs.id_object = ob.id_object                                         " +
                                "   ORDER BY chs.id_choicestudent LIMIT @limit OFFSET @offset";
                NpgsqlCommand npgsqlCommand = new NpgsqlCommand(select, Program.mainForm.con);
                LimitRows = int.Parse(ComBoxLimitRows.SelectedItem.ToString());
                npgsqlCommand.Parameters.AddWithValue("@limit", LimitRows);
                npgsqlCommand.Parameters.AddWithValue("@offset", OffSetRows);
                all_select_button(npgsqlCommand);
                //all_select_button(select);
                dataGridView1.Columns[0].ReadOnly = true;
                dataGridView1.Columns[1].ReadOnly = true;
                dataGridView1.Columns[6].ReadOnly = true;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                additionalHandling = "choicestudent";
                if (OffSetRows >= 0)
                {
                    BtnForward.Enabled = true;
                    BtnForwardAll.Enabled = true;
                    BtnBackAll.Enabled = true;
                }
                BlockBtnForward("choicestudent chs JOIN student s on chs.id_student = s.id_student JOIN object0 ob on chs.id_object = ob.id_object ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void BtnSveazanFilter_Click(object sender, EventArgs e)
        {

        }

        #endregion Методы для работы с формами ввода данных

        /// <summary>
        /// Перевод учащегося в другой класс
        /// </summary>
        /// <param name="dgvr"></param>
        private void StudentDoubleClick(DataGridViewRow dgvr)
        {
            int? id = dgvr.Cells[0].Value as int?;
            if (id != null)
            {
                string studFio = dgvr.Cells[2].Value as string + " " + dgvr.Cells[4].Value + " " + dgvr.Cells[3].Value;
                ChangeClass changeClass = new ChangeClass();
                changeClass.Visible = false;
                changeClass.lblId.Text = id.Value.ToString();
                changeClass.lblFio.Text = studFio;
                changeClass.txtClass.Text = dgvr.Cells[1].Value as string;
                DialogResult dr = changeClass.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    string class0 = changeClass.txtClass.Text.Trim();
                    if (class0 != ((dgvr.Cells[1].Value as string) ?? ""))
                    {
                        int? id_class = null;

                        string selectSql = "select id_class from class0 where class_number = @class_number";
                        using (NpgsqlCommand selCmd = new NpgsqlCommand(selectSql, Program.mainForm.con))
                        {
                            selCmd.Parameters.AddWithValue("class_number", class0);
                            id_class = selCmd.ExecuteScalar() as int?;
                        }
                        if (!id_class.HasValue)
                        {
                            MessageBox.Show("нет такого класса", "error");
                            return;
                        }

                        string updateSql = "update student set id_class = @id_class where id_student = @id_student";
                        using (NpgsqlCommand updCmd = new NpgsqlCommand(updateSql, Program.mainForm.con))
                        {
                            updCmd.Parameters.AddWithValue("id_class", id_class);
                            updCmd.Parameters.AddWithValue("id_student", id);
                            updCmd.ExecuteNonQuery();
                        }
                    }
                }
                else if (dr == DialogResult.Cancel)
                {
                    // ничего не делать
                }
                else
                {
                    MessageBox.Show("DialogResult = " + dr.ToString(), "Error");
                }

            }
        }

        private void ChoiceStudentDoubleClick(DataGridViewRow dgvr)
        {
            int id = (int)dgvr.Cells[0].Value;

            string student = dgvr.Cells[2].Value as string + " " + dgvr.Cells[4].Value + " " +
                                dgvr.Cells[3].Value + " " + dgvr.Cells[5].Value;
            KeyValuePair<int, string> stud = new KeyValuePair<int, string>((int)dgvr.Cells[1].Value, student);
            KeyValuePair<int, string> predmet = new KeyValuePair<int, string>((int)dgvr.Cells[6].Value, dgvr.Cells[7].Value as string);

            ChangeStudentChoice csc = new ChangeStudentChoice();
            csc.lblStudentChoiceId.Text = id.ToString();

            // по студентам
            string sqlStud = "select      s.id_student, c.class_number || ' ' || s.secondnames || ' ' || s.nams || ' ' || s.middlenames   " +
                                "    from    student s                                                                                       " +
                                "    left join    class0 c on s.id_class = c.id_class                                                        " +
                                "    order by c.class_number, s.secondnames, s.nams";
            Program.mainForm.da = new NpgsqlDataAdapter(sqlStud, Program.mainForm.con);
            Program.mainForm.ds.Reset();
            Program.mainForm.da.Fill(Program.mainForm.ds);
            Program.mainForm.dt = Program.mainForm.ds.Tables[0];
            IList<KeyValuePair<int, string>> dsStudList = new List<KeyValuePair<int, string>>();
            foreach (DataRow oneRow in Program.mainForm.dt.Rows)
            {
                dsStudList.Add(new KeyValuePair<int, string>((int)oneRow.ItemArray[0], oneRow.ItemArray[1] as string));
            }
            csc.cbxStudent.DataSource = dsStudList;

            // по предметам
            string sqlObj = "select id_object, title from object0 order by title";
            Program.mainForm.da = new NpgsqlDataAdapter(sqlObj, Program.mainForm.con);
            Program.mainForm.ds.Reset();
            Program.mainForm.da.Fill(Program.mainForm.ds);
            Program.mainForm.dt = Program.mainForm.ds.Tables[0];
            IList<KeyValuePair<int, string>> dsObjList = new List<KeyValuePair<int, string>>();
            foreach (DataRow oneRow in Program.mainForm.dt.Rows)
            {
                dsObjList.Add(new KeyValuePair<int, string>((int)oneRow.ItemArray[0], oneRow.ItemArray[1] as string));
            }
            csc.cbxObject.DataSource = dsObjList;

            // 
            csc.cbxStudent.SelectedItem = stud;
            csc.cbxObject.SelectedItem = predmet;

            DialogResult dr = csc.ShowDialog();
        }

        /// <summary>
        /// всякая доп обработка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridView dgv = sender as DataGridView;
                DataGridViewRow dgvr = dgv.Rows[e.RowIndex];
                string localAdditionalHandling = additionalHandling;
                additionalHandling = "";
                switch (localAdditionalHandling)
                {
                    case "student":
                        StudentDoubleClick(dgvr);
                        break;

                    case "choicestudent":
                        ChoiceStudentDoubleClick(dgvr);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }

        private void Student_Load(object sender, EventArgs e)
        {
            ComBoxLimitRows.Text = ComBoxLimitRows.Items[0].ToString();
        }

        private long getCount(string tableName)
        {
            return (long)(new NpgsqlCommand("select count(*) FROM " + tableName, Program.mainForm.con).ExecuteScalar());
        }

        private void BlockBtnForward(string tableName)
        {

            BlockForward = getCount(tableName);
            if (BlockForward <= NumberRows)
            {
                BtnForward.Enabled = false;
            }
        }

        #region Шаги по таблицам
        private void BtnBack_Click(object sender, EventArgs e)
        {
            OffSetRows -= int.Parse(ComBoxLimitRows.SelectedItem.ToString());
            PageNumber -= 1;
            NumberRows -= LimitRows;
            NotGo = int.Parse(ComBoxLimitRows.SelectedItem.ToString());
            if (LimitRows != NotGo)
            {
                BtnBack.Enabled = false;
                BtnForward.Enabled = false;
                BtnBackAll.Enabled = false;
                BtnForwardAll.Enabled = false;
                return;
            }
            switch (Metod)
            {
                case EnumTabels.Class:
                    BtnClass();

                    break;
                case EnumTabels.Object0:
                    BtnObject();

                    break;
                case EnumTabels.Student:
                    BtnStudent();

                    break;
                case EnumTabels.StudentChoice:
                    BtnStudentChoice();

                    break;
                case EnumTabels.OrderStudentChoice:
                    BtnOrderStudentChoice();

                    break;
                case EnumTabels.OrderStudent:
                    BtnOrderStudent();

                    break;
                case EnumTabels.ConnectFilterStudent:
                    ConnectFilterStudent();

                    break;
                case EnumTabels.ConnectFilterObjectStudent:
                    ConnectFilterObjectStudent();

                    break;

                default:
                    throw new Exception("Нету обработки шаг назад: " + Metod.ToString());
            }
            if (OffSetRows <= 0)
            {
                BtnBack.Enabled = false;
            }
        }

        private void BtnForward_Click(object sender, EventArgs e)
        {
            OffSetRows += int.Parse(ComBoxLimitRows.SelectedItem.ToString());
            PageNumber += 1;
            NumberRows += LimitRows;
            NotGo = int.Parse(ComBoxLimitRows.SelectedItem.ToString());
            if (LimitRows != NotGo)
            {
                BtnBack.Enabled = false;
                BtnForward.Enabled = false;
                BtnBackAll.Enabled = false;
                BtnForwardAll.Enabled = false;
                return;
            }
            switch (Metod)
            {
                case EnumTabels.Class:
                    BtnClass();

                    break;

                case EnumTabels.Object0:
                    BtnObject();

                    break;
                case EnumTabels.Student:
                    BtnStudent();

                    break;
                case EnumTabels.StudentChoice:
                    BtnStudentChoice();

                    break;
                case EnumTabels.OrderStudentChoice:
                    BtnOrderStudentChoice();

                    break;
                case EnumTabels.OrderStudent:
                    BtnOrderStudent();

                    break;
                case EnumTabels.ConnectFilterStudent:
                    ConnectFilterStudent();

                    break;
                case EnumTabels.ConnectFilterObjectStudent:
                    ConnectFilterObjectStudent();

                    break;

                default:
                    throw new Exception("Нету обработки шаг вперёд: " + Metod.ToString());
            }
            if (OffSetRows >= 0)
            {
                BtnBack.Enabled = true;
            }
        }

        private void BtnBackAll_Click(object sender, EventArgs e)
        {
            OffSetRows = 0;
            PageNumber = 1;
            NumberRows = LimitRows;
            switch (Metod)
            {
                case EnumTabels.Class:
                    BtnClass();

                    break;

                case EnumTabels.Object0:
                    BtnObject();

                    break;
                case EnumTabels.Student:
                    BtnStudent();

                    break;
                case EnumTabels.StudentChoice:
                    BtnStudentChoice();

                    break;
                case EnumTabels.OrderStudentChoice:
                    BtnOrderStudentChoice();

                    break;
                case EnumTabels.OrderStudent:
                    BtnOrderStudent();

                    break;
                case EnumTabels.ConnectFilterStudent:
                    ConnectFilterStudent();

                    break;
                case EnumTabels.ConnectFilterObjectStudent:
                    ConnectFilterObjectStudent();

                    break;

                default:
                    throw new Exception("Нету обработки полностью назад: " + Metod.ToString());
            }
            OffSetRowsAll = int.Parse(ComBoxLimitRows.SelectedItem.ToString());
            BtnBack.Enabled = false;
            if(OffSetRowsAll < BlockForward )
            {
                BtnForward.Enabled = true;
            }
        }

        private void BtnForwardAll_Click(object sender, EventArgs e)
        {

            MaxRows = BlockForward;
            PageNumber = 1;
            while(MaxRows > LimitRows)
            {
                MaxRows -= LimitRows;
                PageNumber += 1;
            }

            OffSetRows = BlockForward - MaxRows;
            switch (Metod)
            {
                case EnumTabels.Class:
                    BtnClass();

                    break;

                case EnumTabels.Object0:
                    BtnObject();

                    break;
                case EnumTabels.Student:
                    BtnStudent();

                    break;
                case EnumTabels.StudentChoice:
                    BtnStudentChoice();

                    break;
                case EnumTabels.OrderStudentChoice:
                    BtnOrderStudentChoice();

                    break;
                case EnumTabels.OrderStudent:
                    BtnOrderStudent();

                    break;
                case EnumTabels.ConnectFilterStudent:
                    ConnectFilterStudent();

                    break;
                case EnumTabels.ConnectFilterObjectStudent:
                    ConnectFilterObjectStudent();

                    break;

                default:
                    throw new Exception("Нету обработки полностью вперёд: " + Metod.ToString());
            }
            if (LimitRows < BlockForward)
            {
                BtnBack.Enabled = true;
            }
            BtnForward.Enabled = false;
        }
        #endregion

        #region Не используется
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void tbParam1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ComBoxLimitRows_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void LabPageNumber_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region динамический фильтр
        private void DinamFilterValueStudent()
        {
            //const string strFilterComBoxAll = "SELECT DISTINCT (select c.class_number from class0 c where c.id_class = s.id_class) class_number FROM    student s";
            //const string strFilterComBoxName = "SELECT  s.nams FROM student s join class0 c0 on s.id_class = c0.id_class ORDER BY c0.class_number";
            //const string strFilterConBoxSurname = "SELECT  s.secondnames FROM student s join class0 c0 on s.id_class = c0.id_class ORDER BY c0.class_number";
            //const string strFilterComBoxPatronymic = "SELECT s.middlenames FROM student s join class0 c0 on s.id_class = c0.id_class ORDER BY c0.class_number";

            const string strFilterComBoxAll = "SELECT DISTINCT (select c.class_number from class0 c where c.id_class = s.id_class) class_number FROM    student s";
            const string strFilterComBoxName = "SELECT  s.nams FROM student s join class0 c0 on s.id_class = c0.id_class ORDER BY c0.class_number";
            const string strFilterConBoxSurname = "SELECT  s.secondnames FROM student s join class0 c0 on s.id_class = c0.id_class ORDER BY c0.class_number";
            const string strFilterComBoxPatronymic = "SELECT s.middlenames FROM student s join class0 c0 on s.id_class = c0.id_class ORDER BY c0.class_number";

            FilterComBoxAll(strFilterComBoxAll);
            FilterComBoxName(strFilterComBoxName);
            FilterConBoxSurname(strFilterConBoxSurname);
            FilterComBoxPatronymic(strFilterComBoxPatronymic);
        }

        private void DinamFilterValueStudentObject()
        {
            FilterComBoxAll("SELECT DISTINCT ob.title FROM choicestudent chs, student s, object0 ob " +
                            "WHERE s.id_student=chs.id_student and ob.id_object=chs.id_object");
            FilterComBoxName("SELECT DISTINCT s.nams FROM choicestudent chs, student s, object0 ob " +
                            "WHERE s.id_student=chs.id_student and ob.id_object=chs.id_object");
            FilterConBoxSurname("SELECT DISTINCT s.secondnames FROM choicestudent chs, student s, object0 ob " +
                            "WHERE s.id_student=chs.id_student and ob.id_object=chs.id_object");
            FilterComBoxPatronymic("SELECT DISTINCT s.middlenames FROM choicestudent chs, student s, object0 ob " +
                            "WHERE s.id_student=chs.id_student and ob.id_object=chs.id_object");
        }
        #endregion

        
    }
}
