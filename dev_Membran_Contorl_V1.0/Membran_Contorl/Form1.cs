using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.Threading;
using MySql.Data.MySqlClient;
using System.Drawing.Drawing2D;
using System.IO;
using Microsoft.VisualBasic;
using System.Net.NetworkInformation;
/*2018年7月30日-hcj
1、
2018年7月27日-hcj
1、模压完成更新部件表单
2018年7月22日-hcj
1、增加未连接上服务器提示，
2、更改模压颜色的显示位置和显示颜色以及大小，
3、原本的员工登入YG改为yg和YG，避免大小写输入
2018年7月16日-hcj
1、改变模压板底色，以及增加了样式尺寸
2018年7月31日A-hcj
1、
 * 
 */
namespace Membran_Contorl
{
    public partial class Form1 : Form
    {
        Thread sql_thread;
        MySqlConnection mysql_state;
        MySqlConnection mysql;
        MySqlConnection mysql_mang;
        int Window_width, Window_hight;
        SynchronizationContext m_SyncContext = null;
        Boolean isLogin = false;
        Point Show_bed_Point1 = new Point(0, 0);
        Point Show_bed_Point2 = new Point(0, 0);
        int Show_bed_height = 0;
        int Show_bed_width = 0;
        float Show_ratio = 0;
        String ap_id_this = "";
        String ap_id_last = "";
        String user_id_all = "";
        Boolean isMenbran = false;
        Boolean isFinish = true;
        int total_seg = 0;
        int now_seg = 0;
        int last_seg_no = 80;
        int total_ap_done = 0;
        int color_while_num = 0;
        int[] Start_x = new int[80];
        int[] Start_y = new int[80];
        int[] End_x = new int[80];
        int[] End_y = new int[80];
        int[] Horizontal = new int[80];
        int[] Vertical = new int[80];
        int[] High = new int[80];
        int[] Length = new int[80];
        String[] Id = new String[80];
        String[] Id_scaned = new String[80];
        Graphics Membran_bed;
         String istexture;
        private int color_num_now;
        private int color_num_total_done;
        private string check_day_ago;
        private string this_color;
        private string this_color_last="";
        private string istexture_now_text_last="";
        private string Contract_id;
        private string count_state_num;
        private string count_contract_num;
        private string count_order_state_num;
        private string count_order_num;
        private string Order_id;
        private string info_color;
        private string mysqlStr_manufacture;
        private string string_check_day_info;
        private int color_num_now_unfinish;
        private string mysqlStr_management;
        private bool is_color_exit;
        private int Element_state;
        private bool is_exit_half;

        public Form1()
        {
            InitializeComponent();
            m_SyncContext = SynchronizationContext.Current;
            FormBorderStyle = FormBorderStyle.None;     //设置窗体为无边框样式
            WindowState = FormWindowState.Maximized;
       
            Init_Component_Position();
            mysql_mang = getMySqlCon_mang();
            mysql = getMySqlCon();
            mysql_state = getMySqlCon();
            sql_thread = new Thread(new ThreadStart(SqlListen));
            sql_thread.Start();
       
        }

        private void focus_gun()
        {
            while (!gunReturn.Focused)
            {
                gunReturn.Focus();
            }
        }

        public MySqlConnection getMySqlCon()
        {
            string database_name = Menbran_config.Default.db_name;
            string database_ip = Menbran_config.Default.db_host;
            string database_user = Menbran_config.Default.db_user;
            string database_pass = Menbran_config.Default.db_pass;
            mysqlStr_manufacture = "Database='" + database_name + "';Data Source='" + database_ip + "';User Id='" + database_user + "';Password='" + database_pass + "';CharSet='utf8'";

            String mysqlStr = "Database='" + database_name + "';Data Source='" + database_ip + "';User Id='" + database_user + "';Password='" + database_pass + "';CharSet='utf8'";
            MySqlConnection mysql = new MySqlConnection(mysqlStr);
            return mysql;
        }

        public MySqlConnection getMySqlCon_mang()
        {
            string database_name = Menbran_config.Default.db_mang_name;
            string database_ip = Menbran_config.Default.db_host;
            string database_user = Menbran_config.Default.db_user;
            string database_pass = Menbran_config.Default.db_pass;
            mysqlStr_management = "Database='" + database_name + "';Data Source='" + database_ip + "';User Id='" + database_user + "';Password='" + database_pass + "';CharSet='utf8'";
            String mysqlStr = "Database='" + database_name + "';Data Source='" + database_ip + "';User Id='" + database_user + "';Password='" + database_pass + "';CharSet='utf8'";
            MySqlConnection mysql = new MySqlConnection(mysqlStr);
            return mysql;
        }

        public void SqlListen()
        {
            while (true)
            {
               
                check_day_ago = DateTime.Now.ToString("yyyy-MM-") + (DateTime.Now.Day - Menbran_config.Default.Check_days).ToString();
                //全部未完成
                String sqlSearch_total_unfinish = "SELECT `Index`,`Color`,`Ap_id`,`Texture` FROM `work_membrane_task_list` WHERE (`State`="+Menbran_config.Default.待接单状态+")";
                //当天已完成总数
                String sql_total_ap_done = "SELECT count(*) FROM `work_membrane_task_list` WHERE `Receive_time`>'" + DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00' AND `Receive_time`<'" + DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59' and `State`>" + Menbran_config.Default.待接单状态;
                //当天已完成 颜色+总数
                String sql_total_color_done = "select `Color`,count(*) as count from `work_membrane_task_list` where  `Receive_time`>'" + DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00' AND `Receive_time`<'" + DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59' and `State`>" + Menbran_config.Default.待接单状态 + " group by `Color`";
                //当天未完成总数
                String sql_total_color_unfinish = "select `Color`,count(*) as count from `work_membrane_task_list` where  `State`="+Menbran_config.Default.待接单状态 +" group by `Color`";

                Console.WriteLine(sql_total_color_unfinish);
                MySqlCommand mySqlCommand = getSqlCommand(sqlSearch_total_unfinish, mysql_state);
                MySqlCommand mySqlCommand_total_ap_done = getSqlCommand(sql_total_ap_done, mysql_state);
                MySqlCommand mySqlCommand_total_color_done = getSqlCommand(sql_total_color_done, mysql_state);
                MySqlCommand mySqlCommand_total_color_unfinish = getSqlCommand(sql_total_color_unfinish, mysql_state);
                try
                {
                    mysql_state.Open();
                    getResultset(mySqlCommand, mySqlCommand_total_ap_done, mySqlCommand_total_color_done, mySqlCommand_total_color_unfinish);

                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("MySqlException Error1:" + ex.ToString());
                }
                finally
                {
                    mysql_state.Close();
                }
                Thread.Sleep(3000);
                }
        }

        public static MySqlCommand getSqlCommand(String sql, MySqlConnection mysql)
        {
            MySqlCommand mySqlCommand = new MySqlCommand(sql, mysql);
            return mySqlCommand;
        }

        public void getResultset(MySqlCommand mySqlCommand, MySqlCommand mySqlCommand_total_ap_done, MySqlCommand mySqlCommand_total_color_done, MySqlCommand mySqlCommand_total_color_unfinish)
        {
            m_SyncContext.Post(clearTable, "s");
            int n = 0;
            object to_unfinish_color;
            MySqlDataReader reader_total_color_unfinished = mySqlCommand_total_color_unfinish.ExecuteReader();
            try
            {
                while (reader_total_color_unfinished.Read())
                {
                    if (reader_total_color_unfinished.HasRows)
                    {
                        n = n + 1;
                        String color = reader_total_color_unfinished.GetString(0);
                        int color_num = reader_total_color_unfinished.GetInt32(1);
                        Console.WriteLine(color + ":" + color_num);
                      
                        if (n<=4)
                        {
                            to_unfinish_color = new string[4] { color, color_num.ToString(), "0" ,"1"};
                            m_SyncContext.Post(updateColorTable, to_unfinish_color);
                        }
                       else if (n <= 8)
                        {
                            to_unfinish_color = new string[4] { color, color_num.ToString(), "0" ,"2"};
                            m_SyncContext.Post(updateColorTable, to_unfinish_color);
                        }
                        else if (n <= 12)
                        {
                            to_unfinish_color = new string[4] { color, color_num.ToString(), "0", "3" };
                            m_SyncContext.Post(updateColorTable, to_unfinish_color);
                        }




                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("查询失败了未完成颜色！" + ex.ToString());
            }
            finally
            {
                reader_total_color_unfinished.Close();
            }
            
            MySqlDataReader reader_total_ap_done = mySqlCommand_total_ap_done.ExecuteReader();
            try
            {
                if (reader_total_ap_done.Read())
                {
                    if (reader_total_ap_done.HasRows)
                    {
                        total_ap_done = reader_total_ap_done.GetInt32(0);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("查询失败了总件数！" + ex.ToString());
            }
            finally
            {
                reader_total_ap_done.Close();
            }
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            
            try
            {
                Boolean isRow = false;
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        int index = reader.GetInt32(0);
                        String color = reader.GetString(1);
                        String ap_id = reader.GetString(2);
                        int texture = reader.GetInt32(3);
                        istexture = "无纹";
                        if (texture == 1)
                        {
                            istexture = "有纹";
                        }
                        object toshow = new string[3] { ap_id, color, istexture };
                        isRow = true;
                        m_SyncContext.Post(updateTable, toshow);     
                    }
                }
                if (!isRow)
                {

                    m_SyncContext.Post(update_info, isRow);
                    Console.WriteLine("No Row!");
                }
                else
                {
                    m_SyncContext.Post(update_info, isRow);   
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("查询失败了未完成工单信息！" + ex.ToString());
            }
            finally
            {
                reader.Close();
            }
            object tocolor;
            MySqlDataReader reader_total_color = mySqlCommand_total_color_done.ExecuteReader();
            try
            {
                while (reader_total_color.Read())
                {
                    if (reader_total_color.HasRows)
                    {
                        string color = reader_total_color.IsDBNull(0) ? " " : reader_total_color.GetString(0);
                        int color_num_done = reader_total_color.GetInt32(1);
                        tocolor = new string[2] { color, color_num_done.ToString() };
                        m_SyncContext.Post(updateColorTablefinished, tocolor);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("查询失败了总颜色" + ex.ToString());
            }
            finally
            {
                reader_total_color.Close();
            }

        }

        private void updateTable(object toshowobj)
        {
            string[] toshow = (string[])toshowobj;
            int rows = this.waitTable.Rows.Add();
            for (int i = 0; i < waitTable.ColumnCount; i++)
            {
                waitTable.Rows[rows].Cells[i].Value = toshow[i];
            }
        }

        private void updateColorTable(object toshowobj)
        {
            string[] toshow = (string[])toshowobj;
           
            if (toshow[3]=="1")
            {
                int rows = this.color_info.Rows.Add();
                for (int i = 0; i < color_info.ColumnCount; i++)
                {
                    color_info.Rows[rows].Cells[0].Value = toshow[0];
                    color_info.Rows[rows].Cells[1].Value = toshow[1];
                    color_info.Rows[rows].Cells[2].Value = toshow[2];
                    // color_info.Rows[rows].Cells[3].Value = Convert.ToInt32(color_info.Rows[rows].Cells[1].Value) + Convert.ToInt32(color_info.Rows[rows].Cells[2].Value);
                }
            }
     
        
            if (toshow[3] == "2")
            {
                for (int i = 0; i<=4; i++)
                {
                    if (color_info.Rows[i].Cells[4].Value ==null)
                    {
                        color_info.Rows[i].Cells[4].Value = toshow[0];
                        color_info.Rows[i].Cells[5].Value = toshow[1];
                        color_info.Rows[i].Cells[6].Value = toshow[2];
                        break;
                    }
                    // color_info.Rows[rows].Cells[3].Value = Convert.ToInt32(color_info.Rows[rows].Cells[1].Value) + Convert.ToInt32(color_info.Rows[rows].Cells[2].Value);
                }
            }
            if (toshow[3] == "3")
            {
                for (int i = 0; i <= 4; i++)
                {
                    if (color_info.Rows[i].Cells[8].Value == null)
                    {
                        color_info.Rows[i].Cells[8].Value = toshow[0];
                        color_info.Rows[i].Cells[9].Value = toshow[1];
                        color_info.Rows[i].Cells[10].Value = toshow[2];
                        break;
                    }
                    // color_info.Rows[rows].Cells[3].Value = Convert.ToInt32(color_info.Rows[rows].Cells[1].Value) + Convert.ToInt32(color_info.Rows[rows].Cells[2].Value);
                }
            }




        }

        private void updateColorTablefinished(object toshowobj)
        {
            string[] toshow = (string[])toshowobj;
            
            for (int k = 0; k < color_info.RowCount; k++)
            {
                if (Convert.ToString(color_info.Rows[k].Cells[0].Value)== Convert.ToString(toshow[0]))
                {
                    color_info.Rows[k].Cells[2].Value = toshow[1];
                }
                if (Convert.ToString(color_info.Rows[k].Cells[4].Value) == Convert.ToString(toshow[0]))
                {
                    color_info.Rows[k].Cells[6].Value = toshow[1];
                }
                if (Convert.ToString(color_info.Rows[k].Cells[8].Value) == Convert.ToString(toshow[0]))
                {
                    color_info.Rows[k].Cells[10].Value = toshow[1];
                }
                color_info.Rows[k].Cells[3].Value = Convert.ToInt32(color_info.Rows[k].Cells[1].Value) + Convert.ToInt32(color_info.Rows[k].Cells[2].Value);
                if (color_info.Rows[k].Cells[4].Value!=null)
                {
                    color_info.Rows[k].Cells[7].Value = Convert.ToInt32(color_info.Rows[k].Cells[5].Value) + Convert.ToInt32(color_info.Rows[k].Cells[6].Value);
                }
                if (color_info.Rows[k].Cells[8].Value != null)
                {
                    color_info.Rows[k].Cells[11].Value = Convert.ToInt32(color_info.Rows[k].Cells[9].Value) + Convert.ToInt32(color_info.Rows[k].Cells[10].Value);
                }
               
               

            }


            //color_while_num++;
        }

        private void update_info(object c)
        {
            Boolean cc = (Boolean)c;
            int total_ap_undo = waitTable.RowCount;

            int total_ap_finished = total_ap_done;
            int total_ap = total_ap_finished + total_ap_undo;
            if (total_ap_finished<=0)
            {
                total_ap_finished = 0;
            }
            String ap_info = "当前共" + (total_ap_done+ total_ap_undo) + "条模压工单，余" + total_ap_undo + "条。";
            Console.WriteLine(ap_info);
            state_label.Text = ap_info;
         
            total_process.Maximum = total_ap;
            total_process.Value = total_ap_finished;
       
            
            if (isMenbran)
            {
                if (total_ap_undo <= 1)
                {
                    
                    lastone_label.Visible = true;
                    finish_button.Visible = true;
                 //   Sql_do("UPDATE `work_membrane_task_list` SET `State`=" + Menbran_config.Default.加工完成状态 + ",`Operator_id`='" + user_id_all + "' WHERE `Ap_id` = '" + ap_id_this + "'");

                }
                else
                {
                    lastone_label.Visible = false;
                    finish_button.Visible = false;
                }
            }
            if(cc)
                all_finish_label.Visible = false;
          /*  else
                all_finish_label.Visible = true;
             取消工作全部完成提示   
             */
        }

        private void clearTable(object s)
        {
            color_info.Rows.Clear();
            waitTable.Rows.Clear();
        }
  

        private bool Sql_do(String command)
        {
            MySqlCommand mySqlCommand = getSqlCommand(command, mysql);
            try
            {
                mysql.Open();
                mySqlCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("MySqlException Error3:" + ex.ToString());
                return true;
            }
            finally
            {
                mysql.Close();
            }
            return true;
        }

        private void Init_Component_Position()
        {
            Window_width = Screen.GetBounds(this).Width;
            Window_hight = Screen.GetBounds(this).Height;
            //button_exit.Location = new Point(Window_width - button_exit.Size.Width, Window_hight - button_exit.Size.Height*2);
            gunReturn.Location = new Point(Window_width+100, Window_hight+100);
            // finish_label.Location = new Point(Window_width / 2 - finish_label.Size.Width / 2, Window_hight / 2 - finish_label.Size.Height / 2);
           
            label_ping.Location = new Point(Window_width / 2 - label_ping.Size.Width / 2, Window_hight / 2 - label_ping.Size.Height / 2);
            label_ping.Visible = false;
            lb_error.Location = new Point(Window_width / 2 - label_ping.Size.Width / 2, Window_hight / 2 - label_ping.Size.Height / 2);
            lb_error.Visible = false;
            waitTable.Height = Window_hight / 2;
            waitTable.Width = Window_width / 2;
            waitTable.Location = new Point(Window_width / 2 - waitTable.Size.Width/2, Window_hight / 2 - waitTable.Size.Height/2);
            waitTable.Visible = false;
            user_info.Height = Window_hight / 10;
            user_info.Width = Window_width / 9;
            user_info.Location = new Point(0, 0);
            labelShow1.Location = new Point(Window_width / 2 - labelShow1.Size.Width / 2, Window_hight / 5 - labelShow1.Size.Height / 2);
            Membran_bed = this.CreateGraphics();
            Show_bed_Point1.X = 50;
            Show_bed_Point1.Y = user_info.Size.Height + 50;
            Show_bed_Point2.X = Window_width - 50;
            Show_bed_Point2.Y = Convert.ToInt32((Show_bed_Point2.X - Show_bed_Point1.X) / (Menbran_config.Default.table_width/Menbran_config.Default.table_height));
            Show_bed_height = Show_bed_Point2.Y - Show_bed_Point1.Y;
            Show_bed_width = Show_bed_Point2.X - Show_bed_Point1.X;
            Show_ratio = (float)Show_bed_width / (float)Menbran_config.Default.table_width;
            Console.WriteLine("显示模压床大小：P1" + Show_bed_Point1.X + " " + Show_bed_Point1.Y + " " + Show_bed_Point2.X + " " + Show_bed_Point2.Y + "Ratio" + Show_ratio + "Width:" + Show_bed_width + "Hight:" + Show_bed_height);
            color_label.Location = new Point(20, Window_hight-color_label.Height- color_label_last.Height - 20);
            color_label_last.Location = new Point(20, Window_hight - color_label_last.Height - 20);
            color_label.Visible = false;
            color_label_last.Visible = false;
            lastone_label.Location = new Point(50 + Show_bed_width / 2 - lastone_label.Size.Width / 2, Show_bed_Point2.Y + 10);
            finish_button.Location = new Point(50 + Show_bed_width / 2 - finish_button.Size.Width / 2, Show_bed_Point2.Y + lastone_label.Size.Height + 10);
            all_finish_label.Location = new Point(Window_width / 2 - all_finish_label.Size.Width / 2, Window_hight / 2 - all_finish_label.Size.Height / 2);

            total_process.Location = new Point(Window_width - total_process.Width - 50, state_label.Size.Height+20);
            state_label.Location = new Point(Window_width - state_label.Width-10, 10);
            Version_label.Location = new Point(0, Window_hight - Version_label.Size.Height);
            factory_name.Text = Menbran_config.Default.Factory+"-模压工位";
            factory_name.Location = new Point(Window_width / 2 - factory_name.Size.Width / 2, 0);
            color_info.Location = new Point(user_info.Location.X+user_info.Size.Width,0);
            color_info.Height = Show_bed_Point1.Y;
            finish_label.Location = new Point(50 + Show_bed_width / 2 - lastone_label.Size.Width / 2, Show_bed_Point2.Y + 10);
            finish_label.Visible = false;
            Work_id_show.Location = new Point(Window_width / 2 - Work_id_show.Width / 2, color_label.Location.Y - Work_id_show.Height - 10);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void gunReturn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)(e.KeyChar) == 13)
            {
                
                process_scan(gunReturn.Text.ToString());
                gunReturn.Clear();
            }
        }

        private void process_scan(String code)
        {   
            
            Console.WriteLine("Start process"+code);
            if (code.Contains("YG") || code.Contains("yg"))
            {
             
                String job_id = "";
                if (code.Contains("YG")) { job_id = code.Split('G')[1]; }
                if (code.Contains("yg")) { job_id = code.Split('g')[1]; }
                Boolean is_on = false;
                for (int i = 0; i < user_info.RowCount; i++)
                {
                    if (user_info.Rows[i].Cells[1].Value.ToString() == job_id)
                    {
                        
                        timer1.Interval = 2000;
                        timer1.Start();
                        //下岗当前工单至为完成状态
                        Sql_do("UPDATE `work_membrane_task_list` SET `State`=" + Menbran_config.Default.加工完成状态 + ",`Operator_id`='" + user_id_all + "' WHERE `Ap_id` = '" + ap_id_this + "'");
                        //
                        string sql_update1 = "UPDATE `info_staff_new` SET `is_login_state`=-1,`checkout_time`=now(),`checkin_time`=null WHERE `Job_id`='" + job_id + "'";
                        MySqlHelper.ExecuteNonQuery(mysqlStr_management, CommandType.Text, sql_update1, new MySqlParameter("@prodid", 24));

                        finish_label.Text = user_info.Rows[i].Cells[0].Value.ToString() + "已下班！";
                        finish_label.Visible = true;
                       
                        is_on = true;
                        user_info.Rows.RemoveAt(i);
                        if (user_info.RowCount == 0)
                        {
                            if (isMenbran)
                            {
                                MessageBox.Show("ok！完成后点击确认",
                                "确认下岗！",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                                //sql_thread.Resume();
                            }
                            waitTable.Visible = false;
                            Membran_bed.Clear(SystemColors.Control);
                            //waitTable.Visible = true;
                            ap_id_last = ap_id_this;
                            color_label.Visible = false;
                            color_label_last.Visible = false;
                            state_label.Visible = false;
                            color_info.Visible = false;
                            total_process.Visible = false;
                            Work_id_show.Visible = false;
                            isLogin = false;
                            //sql_thread.Suspend();
                            labelShow1.Visible = false;
                            lastone_label.Visible = false;
                            finish_button.Visible = false;
                            isMenbran = false;
                            labelShow1.Visible = true;
                        }
                    }
                }
                if (!is_on)
                {
                    String sql_user = "SELECT `Position`, `Name` FROM `info_staff_new` WHERE `Job_id`=" + job_id;
                    MySqlCommand mySqlCommand = getSqlCommand(sql_user, mysql_mang);
                    try
                    {
                        mysql_mang.Open();
                        MySqlDataReader reader = mySqlCommand.ExecuteReader();
                        label_ping.Visible = false;
                        while (reader.Read())
                        {
                            if (reader.HasRows)
                            {
                                int auth = reader.GetInt32(0);
                                if (auth == 25 || auth == 15)
                                {
                                String name = reader.GetString(1);
                                    timer1.Interval = 2000;
                                    timer1.Start();
                                    string sql_update1 = "UPDATE `info_staff_new` SET `is_login_state`=1,`checkin_time`=now(),`checkout_time`=null WHERE `Job_id`='" + job_id + "'";
                                    MySqlHelper.ExecuteNonQuery(mysqlStr_management, CommandType.Text, sql_update1, new MySqlParameter("@prodid", 24));
                                    finish_label.Text = name + "已上班！";
                                    finish_label.Visible = true;
                                    /*MessageBox.Show(name + "已上班！",
                                            "3秒后自动关闭！",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);*/
                                    string[] toshow = new string[] { name, job_id };
                                    int rows = this.user_info.Rows.Add();
                                    for (int i = 0; i < user_info.ColumnCount; i++)
                                    {
                                        user_info.Rows[rows].Cells[i].Value = toshow[i];
                                    }
                                    labelShow1.Visible = false;
                                    if(!isMenbran)
                                        waitTable.Visible = true;
                                    isLogin = true;

                                    state_label.Visible = true;
                                    color_info.Visible = true;
                                    total_process.Visible = true;
                                   // Work_id_show.Visible = true;
                                }
                                for (int i = 0; i < user_info.RowCount; i++)
                                {
                                    if (i == 0)
                                        user_id_all = user_info.Rows[i].Cells[1].Value.ToString();
                                    else
                                        user_id_all = user_id_all + "&" + user_info.Rows[i].Cells[1].Value.ToString();
                                }
                            }
                        }

                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine("MySqlException Error1:" + ex.ToString());
                        label_ping.Visible = true;
                    }
                    finally
                    {
                        mysql_mang.Close();
                    }
                }
            }
            else if (code.Contains("EXIT")|| code.Contains("exit"))
            {
                System.Environment.Exit(0);
            }
            else if (code.Contains("SETUP")||code.Contains("setup"))
            {
                setDB();
            }
            else
            {
                String bj_id = code;
                String Id_this = "";
                Boolean isNew_apid = false;
                Boolean change_bed = false;
                Console.WriteLine("部件号扫描成功：" + bj_id);
                Boolean toError = false;
                //sql_thread.Suspend();
                Boolean isFirstMenbran = false;
                if (isLogin)
                {
                    String sql_find_state = "SELECT `State` FROM `order_element_online` WHERE `Code`='" + bj_id+"'";
                    MySqlCommand mySqlCommand_sql_find_state = getSqlCommand(sql_find_state, mysql);
                    try
                    {
                        mysql.Open();
                        MySqlDataReader reader_find_state = mySqlCommand_sql_find_state.ExecuteReader();
                        reader_find_state.Read();
                        if (reader_find_state.HasRows)
                        {
                            Element_state = reader_find_state.GetInt32(0);
                            if (Element_state< Convert.ToInt32(Menbran_config.Default.State_undone))
                            {
                                lb_error.Text = "错误,该零件尚未喷胶!";
                                lb_error.Visible = true;
                                return;
                            }
                            if (Element_state > Convert.ToInt32(Menbran_config.Default.State_done))
                            {
                                lb_error.Text = "错误,该零件已完成模压";
                                lb_error.Visible = true;
                                return;
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine("MySqlException Error1:" + ex.ToString());
                    }
                    finally
                    {
                        mysql.Close();
                    }
                    String sql_find_bj = "SELECT `Membrane_work_order_ap_id`,`Id`,`Contract_id`,`Order_id` FROM `order_element_online` WHERE `Code`='" + bj_id + "'and `Element_type_id` in (1,3,9,4,5,6) and `State`>='" + Menbran_config.Default.State_undone + "'";
                    MySqlCommand mySqlCommand_find_bj = getSqlCommand(sql_find_bj, mysql);
                    try
                    {
                        mysql.Open();
                        MySqlDataReader reader_bj = mySqlCommand_find_bj.ExecuteReader();
                        reader_bj.Read();
                        if (reader_bj.HasRows)
                        {
                            lb_error.Visible = false;

                            ap_id_this = reader_bj.GetString(0);
                            Id_this = reader_bj.GetString(1);
                            Contract_id= reader_bj.GetString(2);
                            Order_id= reader_bj.GetString(3);
                            Console.WriteLine("查询到该部件模压单号：" + ap_id_this + "该部件Id：" + Id_this);
                            if (ap_id_this != ap_id_last)
                            {
                                if (isMenbran)
                                {
                                    //sql_thread.Resume();
                                    change_bed = true;
                                    timer1.Interval = 2000;
                                    timer1.Start();
                                    // 显示对话框
                                    finish_label.Text = "进入下一床！";
                                    finish_label.Visible = true;
                                    /*MessageBox.Show("确认进入下床！",
                                        "3秒后自动关闭！",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);*/
                                }
                                else
                                    ap_id_last = ap_id_this;

                            }


                        }
                        else
                        {
                            Console.WriteLine("无该部件：" + bj_id);


                        }
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine("MySqlException Error1:" + ex.ToString());
                    }
                    finally
                    {
                        mysql.Close();
                    }

                    String unfinish_apid = "";
                    String sql_sure_is_finish = "SELECT `Ap_id` FROM `work_membrane_task_list` WHERE `State`=" + Menbran_config.Default.加工中状态;
                    MySqlCommand mySqlCommand_sure_is_finish = getSqlCommand(sql_sure_is_finish, mysql);
                    try
                    {
                        mysql.Open();
                        MySqlDataReader reader_sure_is_finish = mySqlCommand_sure_is_finish.ExecuteReader();
                        reader_sure_is_finish.Read();
                        if (reader_sure_is_finish.HasRows)
                        {
                            unfinish_apid = reader_sure_is_finish.GetString(0);
                            if (unfinish_apid == ap_id_this)
                            {
                                isNew_apid = false;
                            }
                            else
                            {
                                isNew_apid = true;
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine("MySqlException Error查询进行中工单:" + ex.ToString());
                    }
                    finally
                    {
                        mysql.Close();
                    }



                    String sql_find_menbran_state = "SELECT `State` FROM `work_membrane_task_list` WHERE `Ap_id`='" + ap_id_this + "'";
                    MySqlCommand mySqlCommand_find_membran_state = getSqlCommand(sql_find_menbran_state, mysql);
                    try
                    {
                        mysql.Open();
                        MySqlDataReader reader_membran_state = mySqlCommand_find_membran_state.ExecuteReader();
                        reader_membran_state.Read();
                        if (reader_membran_state.HasRows)
                        {
                            if (reader_membran_state.GetInt32(0) == Convert.ToInt32(Menbran_config.Default.待接单状态) || reader_membran_state.GetInt32(0) == Convert.ToInt32(Menbran_config.Default.加工中状态))
                            {
                                if (!isMenbran)
                                {
                                    waitTable.Visible = false;
                                    isFirstMenbran = true;
                                    isFinish = false;
                                }
                                isMenbran = true;
                            }
                            else
                            {
                                /* isNew_apid = false;
                                 change_bed = false;
                                 */
                                timer1.Interval = 2000;
                                timer1.Start();
                                finish_label.Text = "该部件已完成模压，请报错！";
                                finish_label.Visible = true;
                                DialogResult isError = MessageBox.Show("该部件已完成模压，请报错！",
                                        "请选择",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Information);
                                if (isError == System.Windows.Forms.DialogResult.Yes)
                                {
                                    toError = true;
                                }
                                if (!isMenbran)
                                {
                                    waitTable.Visible = false;
                                    isFirstMenbran = true;
                                    isFinish = false;
                                }
                                isMenbran = true;
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine("MySqlException Error2:" + ex.ToString());
                    }
                    finally
                    {
                        mysql.Close();
                    }
                    if (isNew_apid)
                    {
                        Sql_do("UPDATE `work_membrane_task_list` SET `State`=" + Menbran_config.Default.加工中状态 + " WHERE `Ap_id` = '" + unfinish_apid + "'");

                    }
                    if (toError)
                    {
                        Sql_do("INSERT INTO `info_error_reality` (`Station_Chinese`,`Station_English`,`Find_operator_id`,`Event`,`State`) VALUES ('模压','Membran','" + user_id_all + "','零件" + bj_id + "模压状态错误！','0')");
                    }
                    if (change_bed)
                    {
                        Sql_do("UPDATE `work_membrane_task_list` SET `State`=" + Menbran_config.Default.加工完成状态 + " WHERE `Ap_id` = '" + ap_id_last + "'");

                        now_seg = 0;
                        //isFinish = true;
                        //isMenbran = false;
                        //finish_label.Visible = false;
                        isFirstMenbran = true;
                        Membran_bed.Clear(SystemColors.Control);
                        //waitTable.Visible = true;
                        ap_id_last = ap_id_this;
                        change_bed = false;
                    }
                    if (isMenbran)
                    {
                        Boolean isScaned = false;
                        if (isFirstMenbran)
                        {
                            //sql_thread.Suspend();
                            for (int i = 0; i < user_info.RowCount; i++)
                            {
                                if (i == 0)
                                    user_id_all = user_info.Rows[i].Cells[1].Value.ToString();
                                else
                                    user_id_all = user_id_all + "&" + user_info.Rows[i].Cells[1].Value.ToString();
                            }
                            String time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            Sql_do("UPDATE `work_membrane_task_list` SET `State`=" + Menbran_config.Default.加工中状态 + ", `Receive_time`='" + time + "',`Operator_id`='" + user_id_all + "' WHERE `Ap_id` = '" + ap_id_this + "'");
                            Sql_do("UPDATE `order_element_online` SET `State`="+ Menbran_config.Default.State_done+" ,`Membrane_operator_id`='" + user_id_all + "' ,`Membrane_begin_time`='" + time + "' WHERE `Membrane_work_order_ap_id` = '" + ap_id_this + "' and `Element_type_id` in (1,3,9,4,5,6,13,15,2) ");
                            Sql_do("UPDATE `order_part_online` SET `State`=" + Menbran_config.Default.State_done + "  ,`Membrane_operator_id`='" + user_id_all + "' ,`Membrane_begin_time`='" + time + "' WHERE `Membrane_task_list_ap_id` = '" + ap_id_this + "' and `Element_type_id` in (1,3,9,4,5,6,13,15,2) ");
                            //hcj-2018年7月23日
                            //Sql_do("UPDATE `order_element_online` SET `State`=100 ,`Membrane_operator_id`='" + user_id_all + "' ,`Membrane_begin_time`='" + time + "' WHERE `Membrane_work_order_ap_id` = '" + ap_id_this + "'");
                            update_db_state_ap_id(ap_id_this, Menbran_config.Default.State_done);
                           // Work_id_show.Text = "订单号：" + Order_id;
                            //*hcj

                            mysql.Open();
                            String sql_total_color_all = "select count(*) from `work_membrane_task_list` where  `Color`=" + " (SELECT  `Color` FROM `work_membrane_task_list` WHERE `Ap_id`='" + ap_id_this + "')" + " and `Receive_time`>'" + DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00' AND ( `State`="+Menbran_config.Default.加工完成状态+ " or `State`=" + Menbran_config.Default.加工中状态 + ") and  `Receive_time`<'" + DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59'";
                            MySqlCommand mySqlCommand_find_color_all = getSqlCommand(sql_total_color_all, mysql);
                            MySqlDataReader reader_membran_all = mySqlCommand_find_color_all.ExecuteReader();

                            try
                            {
                                reader_membran_all.Read();
                                if (reader_membran_all.HasRows)
                                {
                                    color_num_total_done = reader_membran_all.GetInt32(0);
                                }
                            }
                            catch (MySqlException ex)
                            {
                                Console.WriteLine("MySqlException Error2:" + ex.ToString());
                            }
                            finally
                            {
                                reader_membran_all.Close();
                            }
                            String sql_total_color_unfinish = "select count(*) from `work_membrane_task_list` where  `Color`=" + " (SELECT  `Color` FROM `work_membrane_task_list` WHERE `Ap_id`='" + ap_id_this + "')" + " and `State`=" + Menbran_config.Default.待接单状态 ;
                            MySqlCommand mySqlCommand_find_color_unfinish = getSqlCommand(sql_total_color_unfinish, mysql);
                            MySqlDataReader reader_membranunfinish = mySqlCommand_find_color_unfinish.ExecuteReader();
                          
                            try
                            {
                                   reader_membranunfinish.Read();
                                if (reader_membranunfinish.HasRows)
                                {
                                    color_num_now_unfinish= reader_membranunfinish.GetInt32(0);
                                    color_num_now =color_num_total_done+ color_num_now_unfinish;
                                    
                                }
                            }
                            catch (MySqlException ex)
                            {
                                Console.WriteLine("MySqlException Error2:" + ex.ToString());
                            }
                            finally
                            {
                                reader_membranunfinish.Close();
                            }
                            //*hcj
                            String sql_find_menbran_data = "SELECT * FROM `work_membrane_task_list` WHERE `Ap_id`='" + ap_id_this + "'";
                            MySqlCommand mySqlCommand_find_membran = getSqlCommand(sql_find_menbran_data, mysql);
                            try
                            {
                               // mysql.Open();
                                MySqlDataReader reader_membran = mySqlCommand_find_membran.ExecuteReader();
                                reader_membran.Read();
                                if (reader_membran.HasRows)
                                {

                                    int istexture_now = reader_membran.GetInt32(2);
                                    string  istexture_now_text = "";
                                    if (istexture_now == 1)
                                    {
                                        istexture_now_text = "有纹";
                                    }
                                    if (istexture_now == 0)
                                    {
                                        istexture_now_text = "无纹";
                                    }
                                    this_color = reader_membran.GetString(1);
                                    

                                    total_seg = reader_membran.GetInt32(10);
                                    Console.WriteLine("该工单共有：" + total_seg);
                                    Membran_bed.FillRectangle(new SolidBrush(Color.Black), Show_bed_Point1.X, Show_bed_Point1.Y, Show_bed_width, Show_bed_height);

                                    //  color_label.Text = "膜皮颜色：" + this_color+istexture;
                                   /* for (int i = 0; i < color_info.RowCount; i++)
                                    {
                                        if (color_info.Rows[i].Cells[0].Value.ToString() == this_color)
                                        {
                                            color_num_total_done = Convert.ToInt32(color_info.Rows[i].Cells[1].Value);
                                        }
                                    }
                                    */
                                    for (int i = 0; i < color_info.RowCount; i++)
                                    {
                                        if (Convert.ToString(color_info.Rows[i].Cells["Color_name_1"].Value) == this_color)
                                        {
                                            color_info.Rows[i].Cells["Unfinish"].Value = color_num_now_unfinish;
                                            break;
                                        }
                                    }

                                    info_color = this_color + "-" + istexture_now_text + "(" + Convert.ToInt32(color_num_total_done) + "/" + color_num_now + ")";
                                    color_label.Text = "摆盘:" + info_color;
                                    color_label_last.Text = "敷膜:" + this_color_last;
                                    this_color_last = info_color;
                                    istexture_now_text_last = istexture_now_text;
                                    color_label_last.Visible = true;
                                    color_label.Visible = true;
                                    Work_id_show.Visible = false;
                                    for (int i = 0; i < total_seg; i++)
                                    {
                                        String PartInfomation = reader_membran.GetString(14 + i);
                                        String[] PartInfs = PartInfomation.Split('&');
                                        int X = Convert.ToInt32(PartInfs[0]);
                                        int Y = Convert.ToInt32(PartInfs[1]);
                                        int R = Convert.ToInt32(PartInfs[4]);
                                        String lines_state;
                                        float H = 0;
                                        float V = 0;

                                        if (R == 1)
                                        {
                                            H = (float)Convert.ToDouble(PartInfs[2]);
                                            V = (float)Convert.ToDouble(PartInfs[3]);
                                        }
                                        else
                                        {
                                            H = (float)Convert.ToDouble(PartInfs[3]);
                                            V = (float)Convert.ToDouble(PartInfs[2]);
                                        }
                                 


                                        Console.WriteLine("X" + X * Show_ratio + "Y" + Y * Show_ratio + "Ratio:" + Show_ratio);
                                        Start_x[i] = Convert.ToInt32(X * Show_ratio) + Show_bed_Point1.X;
                                        Start_y[i] = Convert.ToInt32(Y * Show_ratio) + Show_bed_Point1.Y;
                                        Vertical[i] = Convert.ToInt32(V * Show_ratio);
                                        Horizontal[i] = Convert.ToInt32(H * Show_ratio);
                                        High[i] = Convert.ToInt32((float)Convert.ToDouble(PartInfs[2]));
                                        Length[i] = Convert.ToInt32((float)Convert.ToDouble(PartInfs[3]));
                                      

                                        End_x[i] = Start_x[i] + Convert.ToInt32(Horizontal[i] * Show_ratio);
                                        End_y[i] = Start_y[i] + Convert.ToInt32(Vertical[i] * Show_ratio);
                                        Id[i] = PartInfs[5];
                                        Console.WriteLine("CAL: X" + Start_x[i] + " Y" + Start_y[i] + " EX" + End_x[i] + "EY" + End_y[i]);

                                        draw_rangle(Start_x[i], Start_y[i], Horizontal[i], Vertical[i], High[i], Length[i]);
                                        /*Membran_bed.DrawRectangle(new Pen(new SolidBrush(Color.White),5), Start_x[i], Start_y[i], Horizontal[i], Vertical[i]);
                                     
                                        RectangleF drawRect = new RectangleF(Start_x[i], Start_y[i], Horizontal[i], Vertical[i]);
                                        StringFormat drawFormat = new StringFormat(StringFormatFlags.NoClip);                                                               
                                        drawFormat.Alignment = StringAlignment.Center;
                                        Membran_bed.DrawString(High[i].ToString() + "×" + Length[i].ToString(), new Font("Verdana", 40), new SolidBrush(Color.White), drawRect, drawFormat); 
                                         */



                                    }
                                }
                            }
                            catch (MySqlException ex)
                            {
                                Console.WriteLine("MySqlException Error2:" + ex.ToString());
                            }
                            finally
                            {
                                mysql.Close();
                            }

                        }
                        isScaned = false;
                        for (int i = 0; i < now_seg; i++)
                        {
                            Console.WriteLine(Id_scaned[i]);
                            if (Id_scaned[i] == Id_this)
                            {
                                Console.WriteLine("扫描到重复条码！" + ap_id_this);
                                isScaned = true;
                            }
                        }
                        for (int i = 0; i < total_seg; i++)
                        {
                            if (Id_this == Id[i])
                            {
                                if (!isFirstMenbran)
                                    Membran_bed.DrawRectangle(new Pen(new SolidBrush(Color.Peru),8), Start_x[last_seg_no], Start_y[last_seg_no], Horizontal[last_seg_no], Vertical[last_seg_no]);
                            
                                else
                                    isFirstMenbran = false;
                                Membran_bed.DrawRectangle(new Pen(new SolidBrush(Color.Green),8), Start_x[i], Start_y[i], Horizontal[i], Vertical[i]);

                          
                                last_seg_no = i;
                                if (!isScaned)
                                {
                                    Id_scaned[now_seg] = Id_this;
                                    now_seg++;

                                }
                            }
                        }
                        Console.WriteLine("当前已扫描：" + now_seg);
                       
                    }
                    //else
                    //sql_thread.Resume();
                }
                else
                {
                    timer1.Interval = 2000;
                    timer1.Start();
                    // 显示对话框
                    finish_label.Text = "请先扫描工号牌登陆！";
                    finish_label.Visible = true;
                    /*MessageBox.Show("请先扫描工号牌登陆！",
                        "3秒后自动关闭！",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);*/
                }
            }
            /*else if (code.Contains("FINISH"))
            {
                if (isMenbran)
                {
                    Sql_do("UPDATE `work_membrane_task_list` SET `State`=10 WHERE `Ap_id` = '" + ap_id_this + "'");
                    now_seg = 0;
                    isFinish = true;
                    isMenbran = false;
                    finish_label.Visible = false;
                    Membran_bed.Clear(SystemColors.Control);
                    waitTable.Visible = true;
                    //sql_thread.Resume();
                }
            }*/
            
        }
        private void update_Order_sheet(string Order,string State)
        {
            String sql_count_order_num = "select COUNT(*) from `order_element_online` where `Order_id`='" + Order + "' and `Element_type_id` in (1,3,9,4,5,6)";
            String sql_count_order_state_num = "select COUNT(*) from `order_element_online` where `Order_id`='" + Order + "' and `Element_type_id` in (1,3,9,4,5,6) and `State`=" + State;
            ////////////////////////////////////////////////
            DataSet ds_count_order_num = MySqlHelper.GetDataSet(mysqlStr_manufacture, CommandType.Text, sql_count_order_num, new MySqlParameter("@prodid", 24));
            DataTable dt_count_order_num = ds_count_order_num.Tables[0];
            count_order_num = Convert.ToString(dt_count_order_num.Rows[0][0]);
            DataSet ds_count_order_state_num = MySqlHelper.GetDataSet(mysqlStr_manufacture, CommandType.Text, sql_count_order_state_num, new MySqlParameter("@prodid", 24));
            DataTable dt_count_order_state_num = ds_count_order_state_num.Tables[0];
            count_order_state_num = Convert.ToString(dt_count_order_state_num.Rows[0][0]);

            if (count_order_state_num == count_order_num)//更新 组件 订单库
            {
                Console.WriteLine("所有零件都完成状态，组件 订单状态改变");
                Sql_do("UPDATE `order_order_online` SET `State`=" + State + " WHERE `Order_id` = '" + Order + "'");
                Sql_do("UPDATE `order_section_online` SET `State`=" + State + " WHERE `Order_id` = '" + Order + "'");
            }
        }//更新订单表单
        private void update_Contract_sheet(string Contract,string State)
        {
            String sql_count_contract_num = "select COUNT(*) from `order_element_online` where `Contract_id`='" + Contract + "' and `Element_type_id` in (1,3,9,4,5,6)";
            String sql_count_state_num = "select COUNT(*) from `order_element_online` where `Contract_id`='" + Contract + "' and `Element_type_id` in (1,3,9,4,5,6) and `State`=" + State;

              ////////////////////////////////////////////////
            DataSet ds_count_contract_num = MySqlHelper.GetDataSet(mysqlStr_manufacture, CommandType.Text, sql_count_contract_num, new MySqlParameter("@prodid", 24));
            DataTable dt_count_contract_num = ds_count_contract_num.Tables[0];
            count_contract_num = Convert.ToString(dt_count_contract_num.Rows[0][0]);

            DataSet ds_count_state_num = MySqlHelper.GetDataSet(mysqlStr_manufacture, CommandType.Text, sql_count_state_num, new MySqlParameter("@prodid", 24));
            DataTable dt_count_state_num = ds_count_state_num.Tables[0];
            count_state_num = Convert.ToString(dt_count_state_num.Rows[0][0]);

            Console.WriteLine("count_state_num=" + count_state_num + "count_contract_num=" + count_contract_num);
            if (count_state_num == count_contract_num)//当所有零件都完成状态，则合同状态改变
            {
                Console.WriteLine("所有零件都完成状态，合同状态改变");
                Sql_do("UPDATE `order_contract_internal` SET `State`=" + State + " WHERE `Contract_id` = '" + Contract_id + "'");

            }
        }
        private void update_db_state_ap_id(string ap_id, string State)
        {
            String sql_select_order = "select `Order_id` from `order_element_online` where `Membrane_work_order_ap_id`='" + ap_id + " ' group by `Order_id`";
            DataSet ds_order = MySqlHelper.GetDataSet(mysqlStr_manufacture, CommandType.Text, sql_select_order, new MySqlParameter("@prodid", 24));
            DataTable dt_ds_order = ds_order.Tables[0];
            if (dt_ds_order.Rows.Count>0)
            {
                for (int i = 0; i < dt_ds_order.Rows.Count; i++)
                {
                    update_Order_sheet(Convert.ToString(dt_ds_order.Rows[i]["Order_id"]), State);
                }
            }

            String sql_select_contract = "select `Contract_id` from `order_element_online` where `Membrane_work_order_ap_id`='" + ap_id + " ' group by `Contract_id`";
            DataSet ds_contract = MySqlHelper.GetDataSet(mysqlStr_manufacture, CommandType.Text, sql_select_contract, new MySqlParameter("@prodid", 24));
            DataTable dt_ds_contract = ds_contract.Tables[0];
            if (dt_ds_contract.Rows.Count > 0)
            {
                for (int i = 0; i < dt_ds_contract.Rows.Count; i++)
                {
                    update_Contract_sheet(Convert.ToString(dt_ds_contract.Rows[i]["Contract_id"]), State);
                }
            }


        }

        private void update_db_state_all(string State)
        {
           
            
                //合同
            String sql_count_contract_num = "select COUNT(*) from `order_element_online` where `Contract_id`='" + Contract_id + "' and `Element_type_id` in (1,3,9,4,5,6)";
            String sql_count_state_num = "select COUNT(*) from `order_element_online` where `Contract_id`='" + Contract_id + "' and `Element_type_id` in (1,3,9,4,5,6) and `State`=" + State;
            //订单
            String sql_count_order_num = "select COUNT(*) from `order_element_online` where `Order_id`='" + Order_id + "' and `Element_type_id` in (1,3,9,4,5,6)";
            String sql_count_order_state_num = "select COUNT(*) from `order_element_online` where `Order_id`='" + Order_id + "' and `Element_type_id` in (1,3,9,4,5,6) and `State`=" + State;

            MySqlCommand mySqlCommand_count_order_state_num = getSqlCommand(sql_count_order_state_num, mysql);
            try
            {
                mysql.Open();
                MySqlDataReader reader_count_order_state_num = mySqlCommand_count_order_state_num.ExecuteReader();
                reader_count_order_state_num.Read();
                if (reader_count_order_state_num.HasRows)
                {
                    count_order_state_num = reader_count_order_state_num.GetString(0);
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("MySqlException Error查询进行中工单:" + ex.ToString());
            }
            finally
            {
                mysql.Close();
            }

            MySqlCommand mySqlCommand_count_order_num = getSqlCommand(sql_count_order_num, mysql);
            try
            {
                mysql.Open();
                MySqlDataReader reader_count_order_num = mySqlCommand_count_order_num.ExecuteReader();
                reader_count_order_num.Read();
                if (reader_count_order_num.HasRows)
                {
                    count_order_num = reader_count_order_num.GetString(0);
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("MySqlException Error查询进行中工单:" + ex.ToString());
            }
            finally
            {
                mysql.Close();
            }

            MySqlCommand mySqlCommand_count_state_num = getSqlCommand(sql_count_state_num, mysql);
            try
            {
                mysql.Open();
                MySqlDataReader reader_count_state_num = mySqlCommand_count_state_num.ExecuteReader();
                reader_count_state_num.Read();
                if (reader_count_state_num.HasRows)
                {
                    count_state_num = reader_count_state_num.GetString(0);
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("MySqlException Error查询进行中工单:" + ex.ToString());
            }
            finally
            {
                mysql.Close();
            }



            MySqlCommand mySqlCommand_count_contract_num = getSqlCommand(sql_count_contract_num, mysql);
            try
            {
                mysql.Open();
                MySqlDataReader reader_count_contract_num = mySqlCommand_count_contract_num.ExecuteReader();
                reader_count_contract_num.Read();
                if (reader_count_contract_num.HasRows)
                {
                    count_contract_num = reader_count_contract_num.GetString(0);
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("MySqlException Error查询进行中工单:" + ex.ToString());
            }
            finally
            {
                mysql.Close();
            }
            Console.WriteLine("count_state_num=" + count_state_num + "count_contract_num=" + count_contract_num);
            if (count_state_num == count_contract_num)//当所有零件都完成状态，则合同状态改变
            {
                Console.WriteLine("所有零件都完成状态，合同状态改变");
                Sql_do("UPDATE `order_contract_internal` SET `State`=" + State + " WHERE `Contract_id` = '" + Contract_id + "'");

            }
            if (count_order_state_num == count_order_num)//更新 组件 订单库
            {
                Console.WriteLine("所有零件都完成状态，组件 订单状态改变");
                Sql_do("UPDATE `order_order_online` SET `State`=" + State + " WHERE `Order_id` = '" + Order_id + "'");
                Sql_do("UPDATE `order_section_online` SET `State`=" + State + " WHERE `Order_id` = '" + Order_id + "'");

            }
        }
        private void draw_rangle(int Start_X, int Stary_Y, int Horizontal, int Vertical, int High, int Length)
        {
            int State = 1;//1代表旋转90 0代表不转
            int font_num = 40;
            string write_date = High + "×" + Length;
            int font_show_length = write_date.Length * font_num;
           
            if (Vertical >=font_show_length && Horizontal >=font_show_length) { State = 1; }
            if (Vertical >=font_show_length && Horizontal <font_show_length)
            {
                State = 1;
                if(Horizontal< font_num)
                {
                    font_num = Horizontal-2;
                }
               
            }
            if (Vertical <font_show_length && Horizontal >=font_show_length)
            {
                State = 0;
                if (Vertical < font_num)
                {
                    font_num = Vertical-2;
                }
            }
            if (Vertical <font_show_length && Horizontal <font_show_length)
            {
                if (Vertical + 20 >= Horizontal ) { State = 1; font_num = Vertical /(write_date.Length)-3; }
                if (Vertical + 20 < Horizontal ) { State = 0; font_num = Horizontal / (write_date.Length)-3; }
            }
            if (State == 1)
            {

                Membran_bed.DrawRectangle(new Pen(new SolidBrush(Color.White), 5), Start_X, Stary_Y, Horizontal, Vertical);
                Membran_bed.TranslateTransform(Start_X, Stary_Y + Vertical);
                Membran_bed.RotateTransform(-90);
                RectangleF drawRect = new RectangleF(0, 0, Vertical, Horizontal);
                // Membran_bed.DrawRectangle(new Pen(new SolidBrush(Color.Yellow), 5), 0, 0, Vertical, Horizontal);
                StringFormat drawFormat = new StringFormat(StringFormatFlags.NoClip);
                drawFormat.Alignment = StringAlignment.Center;
                drawFormat.LineAlignment = StringAlignment.Center;
                Membran_bed.DrawString(write_date, new Font("Verdana", font_num), new SolidBrush(Color.White), drawRect, drawFormat);
                Membran_bed.RotateTransform(90);
                Membran_bed.TranslateTransform(-Start_X, -Stary_Y - Vertical);
            }
            else
            {
                Membran_bed.DrawRectangle(new Pen(new SolidBrush(Color.White), 5), Start_X, Stary_Y, Horizontal, Vertical);
                RectangleF drawRect = new RectangleF(Start_X, Stary_Y, Horizontal, Vertical);
                // Membran_bed.DrawRectangle(new Pen(new SolidBrush(Color.Yellow), 5), 0, 0, Vertical, Horizontal);
                StringFormat drawFormat = new StringFormat(StringFormatFlags.NoClip);
                drawFormat.Alignment = StringAlignment.Center;
                drawFormat.LineAlignment = StringAlignment.Center;
                Membran_bed.DrawString(write_date, new Font("Verdana", font_num), new SolidBrush(Color.White), drawRect, drawFormat);

            }


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            //SendKeys.Send("{ESC}");
            finish_label.Visible = false;
        }

        private void finish_button_Click(object sender, EventArgs e)
        {
            if (isMenbran)
            {
                
               // Sql_do("UPDATE `work_membrane_task_list` SET `State`="+Menbran_config.Default.加工完成状态+" WHERE `Ap_id` = '" + ap_id_this + "'");
                now_seg = 0;
                isFinish = true;
                isMenbran = false;
                finish_label.Visible = false;
                Membran_bed.Clear(SystemColors.Control);
                finish_label.Text = "当前工单已全部完成，请下岗。";
                finish_label.Visible = true;
                finish_button.Visible = false;
                lastone_label.Visible = false;
                state_label.Visible = false;
                color_info.Visible = false;
                total_process.Visible = false;
                color_label.Visible = false;
                color_label_last.Visible = false;
                //waitTable.Visible = true;
                //sql_thread.Resume();
            }
        }

        private void setDB()
        {
            string database_name = Menbran_config.Default.db_name;
            string database_ip = Menbran_config.Default.db_host;
            string database_user = Menbran_config.Default.db_user;
            string database_pass = Menbran_config.Default.db_pass;
            database_ip = Interaction.InputBox("请输入服务器IP", "数据库服务器修改", database_ip, 100, 100);
            Menbran_config.Default.db_host = database_ip;
            database_name = Interaction.InputBox("请输入数据库名", "数据库服务器修改", database_name, 100, 100);
            Menbran_config.Default.db_name = database_name;
            database_user = Interaction.InputBox("请输入登陆用户名", "数据库服务器修改", database_user, 100, 100);
            Menbran_config.Default.db_user = database_user;
            database_pass = Interaction.InputBox("请输入登陆密码", "数据库服务器修改", database_pass, 100, 100);
            Menbran_config.Default.db_pass = database_pass;
            Menbran_config.Default.Save();
            mysql = getMySqlCon();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void color_label_Click(object sender, EventArgs e)
        {

        }

        private void Version_label_Click(object sender, EventArgs e)
        {

        }

        private void finish_label_Click(object sender, EventArgs e)
        {

        }

        private void color_info_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private Bitmap Base64StringToImage(string inputStr)
        {
            try
            {
                /*FileStream ifs = new FileStream(txtFileName, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(ifs);

                String inputStr = sr.ReadToEnd();*/
                byte[] arr = Convert.FromBase64String(inputStr);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);

                //bmp.Save(txtFileName + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                //bmp.Save(txtFileName + ".bmp", ImageFormat.Bmp);
                //bmp.Save(txtFileName + ".gif", ImageFormat.Gif);
                //bmp.Save(txtFileName + ".png", ImageFormat.Png);
                ms.Close();
                return bmp;
                //sr.Close();
                //ifs.Close();
                //this.pictureBox2.Image = bmp;
                /*if (File.Exists(txtFileName))
                {
                    File.Delete(txtFileName);
                }*/
                //MessageBox.Show("转换成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Base64StringToImage 转换失败\nException：" + ex.Message);
                return null;
            }
        }


    }
}