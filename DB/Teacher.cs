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
    public partial class BtnLesson : Form
    {
        string additionalHandling = "";
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
        EnumConnect MetodConnect = EnumConnect.None;
        public BtnLesson()
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
        private void all_select_button(NpgsqlCommand npgsqlCommand)
        {
            viewOnly();
            Program.mainForm.da = new NpgsqlDataAdapter(npgsqlCommand);

            NpgsqlCommandBuilder npgsqlCommandBuilder = new NpgsqlCommandBuilder(Program.mainForm.da);
            Program.mainForm.ds.Reset();
            Program.mainForm.da.Fill(Program.mainForm.ds);
            Program.mainForm.dt = Program.mainForm.ds.Tables[0];
            dataGridView1.DataSource = Program.mainForm.dt;
        }

        private void all_select_button(string sql)
        {
            viewOnly();
            Program.mainForm.da = new NpgsqlDataAdapter(sql, Program.mainForm.con);
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
        private void all_update_button(NpgsqlCommand npgsqlCommand)
        {
            allowEdit();
            Program.mainForm.da = new NpgsqlDataAdapter(npgsqlCommand);

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
        }

        #endregion Работа с отчетами и формами ввода

        /// <summary>
        /// редактировать список предметов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnObject_Click(object sender, EventArgs e)
        {
            PageNumber = 1;

            BtnConnectFilter.Enabled = false;
            BtnBack.Enabled = false;
            BtnForward.Enabled = false;

            ComBoxAll.Text = "";
            ComBoxName.Text = "";
            ComBoxSurname.Text = "";
            ComBoxPatronymic.Text = "";

            Metod = EnumTabels.Object0;
            OffSetRows = 0;
            NumberRows = int.Parse(ComBoxLimitRows.SelectedItem.ToString());
            BtnObjectTecher();
        }

        private void BtnObjectTecher()
        {
            LabPageNumber.Text = "Номер страницы: " + PageNumber;
            try
            {
                PrepareGrid();
                string select = "SELECT ob.id_object, ob.title  " +
                                "   FROM object0 ob             " +
                                "   Order by ob.title LIMIT @limit OFFSET @offset;";
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



        private void BtnTeacher_Click(object sender, EventArgs e)
        {
            PageNumber = 1;

            BtnConnectFilter.Enabled = false;
            BtnBack.Enabled = false;
            BtnForward.Enabled = false;

            ComBoxAll.Text = "";
            ComBoxName.Text = "";
            ComBoxSurname.Text = "";
            ComBoxPatronymic.Text = "";

            Metod = EnumTabels.Teacher;
            OffSetRows = 0;
            NumberRows = int.Parse(ComBoxLimitRows.SelectedItem.ToString());
            BtnTeacherMetod();
        }

        private void BtnTeacherMetod()
        {
            LabPageNumber.Text = "Номер страницы: " + PageNumber;
            try
            {
                PrepareGrid();
                string select = "SELECT te.id_teacher, te.namt, te.middlenamet, te.secondnamet, te.logint, te.passwordt " +
                                "   FROM teacher te                                                                     " +
                                "   ORDER BY te.secondnamet LIMIT @limit OFFSET @offset;";
                NpgsqlCommand npgsqlCommand = new NpgsqlCommand(select, Program.mainForm.con);
                LimitRows = int.Parse(ComBoxLimitRows.SelectedItem.ToString());
                npgsqlCommand.Parameters.AddWithValue("@limit", LimitRows);
                npgsqlCommand.Parameters.AddWithValue("@offset", OffSetRows);
                all_update_button(npgsqlCommand);
                dataGridView1.Columns[0].ReadOnly = true;
                dataGridView1.Columns[0].Visible = false;
                additionalHandling = "teacher";
                if (OffSetRows >= 0)
                {
                    BtnForward.Enabled = true;
                    BtnForwardAll.Enabled = true;
                    BtnBackAll.Enabled = true;
                }
                BlockBtnForward("teacher");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void PrepareGrid()
        {
            try
            {
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.ReadOnly = false;
                }
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            PageNumber = 1;

            BtnConnectFilter.Enabled = false;
            BtnBack.Enabled = false;
            BtnForward.Enabled = false;

            ComBoxAll.Text = "";
            ComBoxName.Text = "";
            ComBoxSurname.Text = "";
            ComBoxPatronymic.Text = "";

            Metod = EnumTabels.Lesson;
            OffSetRows = 0;
            NumberRows = int.Parse(ComBoxLimitRows.SelectedItem.ToString());
            BtnLessonTeacher();
        }

        private void BtnLessonTeacher()
        {
            LabPageNumber.Text = "Номер страницы: " + PageNumber;
            try
            {
                PrepareGrid();
                string select = "SELECT ls.id_lesson,                                       " +
                                "       ob.id_object, ob.title,                             " +
                                "       t.id_teacher, t.secondnamet, t.namt, t.middlenamet  " +
                                "   FROM lesson ls                                          " +
                                "   JOIN object0 ob on ls.id_object = ob.id_object          " +
                                "   JOIN teacher t on ls.id_teacher = t.id_teacher          " +
                                "   ORDER BY t.secondnamet, t.namt, t.middlenamet LIMIT @limit OFFSET @offset";
                NpgsqlCommand npgsqlCommand = new NpgsqlCommand(select, Program.mainForm.con);
                LimitRows = int.Parse(ComBoxLimitRows.SelectedItem.ToString());
                npgsqlCommand.Parameters.AddWithValue("@limit", LimitRows);
                npgsqlCommand.Parameters.AddWithValue("@offset", OffSetRows);
                all_select_button(npgsqlCommand);

                dataGridView1.Columns[0].ReadOnly = true;
                dataGridView1.Columns[1].ReadOnly = true;
                dataGridView1.Columns[3].ReadOnly = true;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[3].Visible = false;
                additionalHandling = "lesson";
                if (OffSetRows >= 0)
                {
                    BtnForward.Enabled = true;
                    BtnForwardAll.Enabled = true;
                    BtnBackAll.Enabled = true;
                }
                BlockBtnForward("lesson ls JOIN object0 ob on ls.id_object = ob.id_object JOIN teacher t on ls.id_teacher = t.id_teacher");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void BtnLessonVid_Click(object sender, EventArgs e)
        {

            PageNumber = 1;

            BtnConnectFilter.Enabled = true;
            BtnBack.Enabled = false;
            BtnForward.Enabled = false;

            ComBoxAll.Text = "";
            ComBoxName.Text = "";
            ComBoxSurname.Text = "";
            ComBoxPatronymic.Text = "";
            

            Metod = EnumTabels.LessonVid;
            OffSetRows = 0;
            NumberRows = int.Parse(ComBoxLimitRows.SelectedItem.ToString());
            BtnLessonVidTeacher();
            FilterComBoxAll("SELECT title FROM object0 ORDER BY title");
            FilterComBoxName("SELECT namt FROM teacher ORDER BY namt");
            FilterConBoxSurname("SELECT secondnamet FROM teacher ORDER BY secondnamet");
            FilterComBoxPatronymic("SELECT middlenamet FROM teacher ORDER BY middlenamet");
        }

        private void BtnLessonVidTeacher()
        {
            LabPageNumber.Text = "Номер страницы: " + PageNumber;
            try
            {
                PrepareGrid();
                string sql = "SELECT ls.id_lesson, ob.title, te.namt, te.secondnamet, te.middlenamet " +
                                "   FROM lesson ls, object0 ob, teacher te " +
                                "   WHERE ls.id_object = ob.id_object AND ls.id_teacher=te.id_teacher LIMIT @limit OFFSET @offset;";
                NpgsqlCommand npgsqlCommand = new NpgsqlCommand(sql, Program.mainForm.con);
                LimitRows = int.Parse(ComBoxLimitRows.SelectedItem.ToString());
                npgsqlCommand.Parameters.AddWithValue("@limit", LimitRows);
                npgsqlCommand.Parameters.AddWithValue("@offset", OffSetRows);
                all_select_button(npgsqlCommand);

                if (OffSetRows >= 0)
                {
                    BtnForward.Enabled = true;
                    BtnForwardAll.Enabled = true;
                    BtnBackAll.Enabled = true;
                }
                BlockBtnForward("lesson ls, object0 ob, teacher te " +
                                "WHERE ls.id_object = ob.id_object AND ls.id_teacher=te.id_teacher");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

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

        }

        private void button2_Click(object sender, EventArgs e)
        {
            PageNumber = 1;

            BtnConnectFilter.Enabled = false;
            BtnBack.Enabled = false;
            BtnForward.Enabled = false;

            ComBoxAll.Text = "";
            ComBoxName.Text = "";
            ComBoxSurname.Text = "";
            ComBoxPatronymic.Text = "";

            Metod = EnumTabels.ClassTeacher;
            OffSetRows = 0;
            NumberRows = int.Parse(ComBoxLimitRows.SelectedItem.ToString());
            BtnClassTeacher();
        }

        private void BtnClassTeacher()
        {
            LabPageNumber.Text = "Номер страницы: " + PageNumber;
            try
            {
                PrepareGrid();
                string select = "select cl.id_classlesson,                                                                                      " +
                                "       c0.id_class, c0.class_number,                                                                           " +
                                "       ls.id_lesson, ob.title || ', ' || t.secondnamet || ' ' || t.namt || ' ' || t.middlenamet as teacher     " +
                                "   from      class_lesson cl                                                                                   " +
                                "   join class0 c0 on cl.id_class = c0.id_class                                                            " +
                                "   join lesson ls on cl.id_lesson = ls.id_lesson                                                          " +
                                "   join object0 ob on ls.id_object = ob.id_object                                                         " +
                                "   join teacher t on ls.id_teacher = t.id_teacher                                                         " +
                                "   order by c0.class_number, ob.title LIMIT @limit OFFSET @offset";
                NpgsqlCommand npgsqlCommand = new NpgsqlCommand(select, Program.mainForm.con);
                LimitRows = int.Parse(ComBoxLimitRows.SelectedItem.ToString());
                npgsqlCommand.Parameters.AddWithValue("@limit", LimitRows);
                npgsqlCommand.Parameters.AddWithValue("@offset", OffSetRows);
                all_select_button(npgsqlCommand);

                dataGridView1.Columns[0].ReadOnly = true;
                dataGridView1.Columns[1].ReadOnly = true;
                dataGridView1.Columns[3].ReadOnly = true;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[3].Visible = false;
                additionalHandling = "class_lesson";
                if (OffSetRows >= 0)
                {
                    BtnForward.Enabled = true;
                    BtnForwardAll.Enabled = true;
                    BtnBackAll.Enabled = true;
                }
                BlockBtnForward("class_lesson cl  join class0 c0 on cl.id_class = c0.id_class join lesson ls on cl.id_lesson = ls.id_lesson" +
                               "join object0 ob on ls.id_object = ob.id_object" +
                                "join teacher t on ls.id_teacher = t.id_teacher");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void BtnClassLesson_Click(object sender, EventArgs e)
        {
            PageNumber = 1;

            ComBoxAll.Text = "";
            ComBoxName.Text = "";
            ComBoxSurname.Text = "";
            ComBoxPatronymic.Text = "";

            BtnConnectFilter.Enabled = true;
            BtnBack.Enabled = false;
            BtnForward.Enabled = false;

            Metod = EnumTabels.ClassLesson;
            OffSetRows = 0;
            NumberRows = int.Parse(ComBoxLimitRows.SelectedItem.ToString());
            BtnClassLessonTeacher();
            FilterComBoxAll("SELECT class_number FROM class0 ORDER BY class_number");
            FilterComBoxName("SELECT namt FROM teacher ORDER BY namt");
            FilterConBoxSurname("SELECT secondnamet FROM teacher ORDER BY secondnamet");
            FilterComBoxPatronymic("SELECT middlenamet FROM teacher ORDER BY middlenamet");
            
        }

        private void BtnClassLessonTeacher()
        {
            LabPageNumber.Text = "Номер страницы: " + PageNumber;
            try
            {
                PrepareGrid();
                string sql = "SELECT cl.id_classlesson, te.namt, te.secondnamet, te.middlenamet, c0.class_number " +
                                "   FROM class_lesson cl, class0 c0, lesson le, teacher te " +
                                "   WHERE cl.id_lesson=le.id_lesson AND c0.id_class=cl.id_class AND te.id_teacher=le.id_teacher LIMIT @limit OFFSET @offset;";
                NpgsqlCommand npgsqlCommand = new NpgsqlCommand(sql, Program.mainForm.con);
                LimitRows = int.Parse(ComBoxLimitRows.SelectedItem.ToString());
                npgsqlCommand.Parameters.AddWithValue("@limit", LimitRows);
                npgsqlCommand.Parameters.AddWithValue("@offset", OffSetRows);
                all_select_button(npgsqlCommand);

                if (OffSetRows >= 0)
                {
                    BtnForward.Enabled = true;
                    BtnForwardAll.Enabled = true;
                    BtnBackAll.Enabled = true;
                }
                BlockBtnForward("class_lesson cl, class0 c0, lesson le, teacher te " +
                                "   WHERE cl.id_lesson=le.id_lesson AND c0.id_class=cl.id_class AND te.id_teacher=le.id_teacher;");

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

        private void BtnConnectFilter_Click(object sender, EventArgs e)
        {
            switch (Metod)
            {
                case EnumTabels.ClassLesson:
                    ConnectFiltrClass();

                    break;
                case EnumTabels.LessonVid:
                    ConnectFiltrObject();

                    break;
                case EnumTabels.ConnectFilterClass:
                    ConnectFiltrClass();

                    break;
                case EnumTabels.ConnectFilterObject:
                    ConnectFiltrObject();

                    break;

                default:
                    throw new Exception("Нету обработки фильтр: " + Metod.ToString());
            }
            //MetodConnect = EnumConnect.ConnectFilter;
            //Metod = EnumTabels.ConnectFilter;
        }

        private void ConnectFiltrObject()
        {
            Metod = EnumTabels.ConnectFilterObject;
            LabPageNumber.Text = "Номер страницы: " + PageNumber;
            try
            {
                PrepareGrid();

                string sqlPattern = "SELECT {0} " +
                             "   FROM lesson ls, object0 ob, teacher te" +
                             "   WHERE ls.id_object = ob.id_object AND ls.id_teacher=te.id_teacher " +
                             "   AND (ob.title = @title OR @title = '')" +
                             "   AND (te.namt = @namet OR @namet = '')" +
                             "   AND (te.secondnamet = @surname  OR @surname = '')" +
                             "   AND (te.middlenamet = @patronymic OR @patronymic = '')" +
                             "   {1};";


                string sql = string.Format(sqlPattern, "ls.id_lesson, ob.title, te.namt, te.secondnamet, te.middlenamet", "LIMIT @limit OFFSET @offset");
                string sqlCount = string.Format(sqlPattern, "count(*)", "");

                NpgsqlCommand npgsqlCommand = new NpgsqlCommand(sql, Program.mainForm.con);
                LimitRows = int.Parse(ComBoxLimitRows.SelectedItem.ToString());
                SelectAll = ComBoxAll.SelectedItem == null ? "" : ComBoxAll.SelectedItem.ToString();
                SelectName = ComBoxName.SelectedItem == null ? "" : ComBoxName.SelectedItem.ToString();
                SelectSurname = ComBoxSurname.SelectedItem == null ? "" : ComBoxSurname.SelectedItem.ToString();
                SelectPatronymic = ComBoxPatronymic.SelectedItem == null ? "" : ComBoxPatronymic.SelectedItem.ToString();

                npgsqlCommand.Parameters.Add("@title", NpgsqlTypes.NpgsqlDbType.Text, 20);
                npgsqlCommand.Parameters["@title"].Value = SelectAll;

                npgsqlCommand.Parameters.Add("@namet", NpgsqlTypes.NpgsqlDbType.Text, 20);
                npgsqlCommand.Parameters["@namet"].Value = SelectName;

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

        private void ConnectFiltrClass()
        {
            Metod = EnumTabels.ConnectFilterClass;
            LabPageNumber.Text = "Номер страницы: " + PageNumber;
            try
            {
                PrepareGrid();

                string sqlPattern = "SELECT {0} " +
                             "   FROM class_lesson cl, class0 c0, lesson le, teacher te " +
                             "   WHERE cl.id_lesson=le.id_lesson AND c0.id_class=cl.id_class AND te.id_teacher=le.id_teacher " +
                             "   AND (c0.class_number = @class0 OR @class0 = '')" +
                             "   AND (te.namt = @namet OR @namet = '')" +
                             "   AND (te.secondnamet = @surname  OR @surname = '')" +
                             "   AND (te.middlenamet = @patronymic OR @patronymic = '')" +
                             "   {1};";

                string sql = string.Format(sqlPattern, "cl.id_classlesson, te.namt, te.secondnamet, te.middlenamet, c0.class_number", "LIMIT @limit OFFSET @offset");
                string sqlCount = string.Format(sqlPattern, "count(*)", "");
                
                NpgsqlCommand npgsqlCommand = new NpgsqlCommand(sql, Program.mainForm.con);
                LimitRows = int.Parse(ComBoxLimitRows.SelectedItem.ToString());
                SelectAll = ComBoxAll.SelectedItem == null ? "" : ComBoxAll.SelectedItem.ToString();
                SelectName = ComBoxName.SelectedItem == null ? "" : ComBoxName.SelectedItem.ToString();
                SelectSurname = ComBoxSurname.SelectedItem == null ? "" : ComBoxSurname.SelectedItem.ToString();
                SelectPatronymic = ComBoxPatronymic.SelectedItem == null ? "" : ComBoxPatronymic.SelectedItem.ToString();
               
                npgsqlCommand.Parameters.Add("@class0", NpgsqlTypes.NpgsqlDbType.Text, 20);
                npgsqlCommand.Parameters["@class0"].Value = SelectAll;
               
                npgsqlCommand.Parameters.Add("@namet", NpgsqlTypes.NpgsqlDbType.Text, 20);
                npgsqlCommand.Parameters["@namet"].Value = SelectName;
               
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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridView dgv = sender as DataGridView;
                DataGridViewRow dgvr = dgv.Rows[e.RowIndex];
                string tableName = (dgv.DataSource as DataTable).TableName;
                string localAdditionalHandling = additionalHandling;
                additionalHandling = "";
                switch (localAdditionalHandling)
                {
                    case "lesson":
                        LessonDoubleClick(dgvr);
                        break;

                    case "class_lesson":
                        ClassLessonDoubleClick(dgvr);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }

        private void ClassLessonDoubleClick(DataGridViewRow dgvr)
        {
            int id = (int)dgvr.Cells[0].Value;

            int classKey = dgvr.Cells[1].Value is DBNull ? -1 : (int)dgvr.Cells[1].Value;
            string classValue = dgvr.Cells[2].Value is DBNull ? "" : dgvr.Cells[2].Value as string;
            KeyValuePair<int, string> class0 = new KeyValuePair<int, string>(classKey, classValue);

            int teachKey = dgvr.Cells[3].Value is DBNull ? -1 : (int)dgvr.Cells[3].Value;
            string teachValue = dgvr.Cells[4].Value is DBNull ? "" : dgvr.Cells[4].Value as string;
            KeyValuePair<int, string> lesson = new KeyValuePair<int, string>(teachKey, teachValue);

            // по классам
            string sqlClass = "SELECT id_class, class_number FROM class0 order by class_number";
            Program.mainForm.da = new NpgsqlDataAdapter(sqlClass, Program.mainForm.con);
            Program.mainForm.ds.Reset();
            Program.mainForm.da.Fill(Program.mainForm.ds);
            Program.mainForm.dt = Program.mainForm.ds.Tables[0];
            IList<KeyValuePair<int, string>> dsClassList = new List<KeyValuePair<int, string>>();
            foreach (DataRow oneRow in Program.mainForm.dt.Rows)
            {
                dsClassList.Add(new KeyValuePair<int, string>((int)oneRow.ItemArray[0], oneRow.ItemArray[1] as string));
            }

            // по преподавателям
            string sqlTch = "SELECT ls.id_lesson,                                                                           " +
                            "       ob.title || ', ' || t.secondnamet || ' ' || t.namt || ' ' || t.middlenamet as teacher   " +
                            "   from lesson ls                                                                              " +
                            "   join object0 ob on ls.id_object = ob.id_object                                              " +
                            "   join teacher t on ls.id_teacher = t.id_teacher                                              " +
                            "   order by teacher    ";
            Program.mainForm.da = new NpgsqlDataAdapter(sqlTch, Program.mainForm.con);
            Program.mainForm.ds.Reset();
            Program.mainForm.da.Fill(Program.mainForm.ds);
            Program.mainForm.dt = Program.mainForm.ds.Tables[0];
            IList<KeyValuePair<int, string>> dsLessonList = new List<KeyValuePair<int, string>>();
            foreach (DataRow oneRow in Program.mainForm.dt.Rows)
            {
                dsLessonList.Add(new KeyValuePair<int, string>((int)oneRow.ItemArray[0], oneRow.ItemArray[1] as string));
            }

            LessonClass lessonClass = new LessonClass();
            lessonClass.lblId.Text = id.ToString();
            lessonClass.cbxClass.DataSource = dsClassList;
            lessonClass.cbxLesson.DataSource = dsLessonList;

            // 
            lessonClass.cbxClass.SelectedItem = class0;
            lessonClass.cbxLesson.SelectedItem = lesson;

            DialogResult dr = lessonClass.ShowDialog();
        }

        private void LessonDoubleClick(DataGridViewRow dgvr)
        {
            int id = (int)dgvr.Cells[0].Value;

            string teach = dgvr.Cells[4].Value as string + " " + dgvr.Cells[5].Value + " " + dgvr.Cells[6].Value;
            int predmetKey = dgvr.Cells[1].Value is DBNull ? -1 : (int)dgvr.Cells[1].Value;
            string predmetValue = dgvr.Cells[2].Value is DBNull ? "" : dgvr.Cells[2].Value as string;
            KeyValuePair<int, string> predmet = new KeyValuePair<int, string>(predmetKey, predmetValue);
            KeyValuePair<int, string> teacher = new KeyValuePair<int, string>((int)dgvr.Cells[3].Value, teach);

            Lessons ls = new Lessons();
            ls.lblLessonsId.Text = id.ToString();

            // по преподавателям 
            string sqlStud = "select      t.id_teacher, t.secondnamet || ' ' || t.namt || ' ' || t.middlenamet " +
                                "    from    teacher t                                                         " +
                                "    order by t.secondnamet, t.namt, t.middlenamet  ";
            Program.mainForm.da = new NpgsqlDataAdapter(sqlStud, Program.mainForm.con);
            Program.mainForm.ds.Reset();
            Program.mainForm.da.Fill(Program.mainForm.ds);
            Program.mainForm.dt = Program.mainForm.ds.Tables[0];
            IList<KeyValuePair<int, string>> dsStudList = new List<KeyValuePair<int, string>>();
            foreach (DataRow oneRow in Program.mainForm.dt.Rows)
            {
                dsStudList.Add(new KeyValuePair<int, string>((int)oneRow.ItemArray[0], oneRow.ItemArray[1] as string));
            }
            ls.cbxTeacher.DataSource = dsStudList;

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
            ls.cbxObject.DataSource = dsObjList;

            // 
            ls.cbxTeacher.SelectedItem = teacher;
            ls.cbxObject.SelectedItem = predmet;

            DialogResult dr = ls.ShowDialog();
        }

        private void Btn10_Click(object sender, EventArgs e)
        {
            
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

        private long getCountConnect(string sqlCount)
        {
            return (long)(new NpgsqlCommand(sqlCount, Program.mainForm.con).ExecuteScalar());
        }

        private void BlockBtnForwardConnect(string sqlCount)
        {

            BlockForward = getCountConnect(sqlCount);
            if (BlockForward <= NumberRows)
            {
                BtnForward.Enabled = false;
            }
        }

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

        #region листание по страницам
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
                case EnumTabels.ClassTeacher:
                    BtnClassTeacher();

                    break;
                case EnumTabels.Object0:
                    BtnObjectTecher();

                    break;
                case EnumTabels.Teacher:
                    BtnTeacherMetod();

                    break;
                case EnumTabels.Lesson:
                    BtnLessonTeacher();

                    break;
                case EnumTabels.LessonVid:
                    BtnLessonVidTeacher();

                    break;
                case EnumTabels.ClassLesson:
                    BtnClassLessonTeacher();

                    break;
                case EnumTabels.ConnectFilterClass:
                    ConnectFiltrClass();

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
                case EnumTabels.ClassTeacher:
                    BtnClassTeacher();

                    break;

                case EnumTabels.Object0:
                    BtnObjectTecher();

                    break;
                case EnumTabels.Teacher:
                    BtnTeacherMetod();

                    break;
                case EnumTabels.Lesson:
                    BtnLessonTeacher();

                    break;
                case EnumTabels.LessonVid:
                    BtnLessonVidTeacher();

                    break;
                case EnumTabels.ClassLesson:
                    BtnClassLessonTeacher();

                    break;
                case EnumTabels.ConnectFilterClass:
                    ConnectFiltrClass();

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
                case EnumTabels.ClassTeacher:
                    BtnClassTeacher();

                    break;

                case EnumTabels.Object0:
                    BtnObjectTecher();

                    break;
                case EnumTabels.Teacher:
                    BtnTeacherMetod();

                    break;
                case EnumTabels.Lesson:
                    BtnLessonTeacher();

                    break;
                case EnumTabels.LessonVid:
                    BtnLessonVidTeacher();

                    break;
                case EnumTabels.ClassLesson:
                    BtnClassLessonTeacher();

                    break;
                case EnumTabels.ConnectFilterClass:
                    ConnectFiltrClass();

                    break;

                default:
                    throw new Exception("Нету обработки полностью назад: " + Metod.ToString());
            }
            OffSetRowsAll = int.Parse(ComBoxLimitRows.SelectedItem.ToString());
            BtnBack.Enabled = false;
            if (OffSetRowsAll < BlockForward)
            {
                BtnForward.Enabled = true;
            }
        }

        private void BtnForwardAll_Click(object sender, EventArgs e)
        {

            MaxRows = BlockForward;
            PageNumber = 1;
            while (MaxRows > LimitRows)
            {
                MaxRows -= LimitRows;
                PageNumber += 1;
            }

            OffSetRows = BlockForward - MaxRows;
            switch (Metod)
            {
                case EnumTabels.ClassTeacher:
                    BtnClassTeacher();

                    break;

                case EnumTabels.Object0:
                    BtnObjectTecher();

                    break;
                case EnumTabels.Teacher:
                    BtnTeacherMetod();

                    break;
                case EnumTabels.Lesson:
                    BtnLessonTeacher();

                    break;
                case EnumTabels.LessonVid:
                    BtnLessonVidTeacher();

                    break;
                case EnumTabels.ClassLesson:
                    BtnClassLessonTeacher();

                    break;
                case EnumTabels.ConnectFilterClass:
                    ConnectFiltrClass();

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
        private void BtnLesson_Load(object sender, EventArgs e)
        {
            ComBoxLimitRows.Text = ComBoxLimitRows.Items[0].ToString();
        }
    }
}
