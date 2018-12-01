using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Membran_Contorl
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.waitTable = new System.Windows.Forms.DataGridView();
            this.列1工单号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.列1颜色 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.列1纹路 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelShow1 = new System.Windows.Forms.Label();
            this.gunReturn = new System.Windows.Forms.TextBox();
            this.user_info = new System.Windows.Forms.DataGridView();
            this.User_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.User_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.finish_label = new System.Windows.Forms.Label();
            this.state_label = new System.Windows.Forms.Label();
            this.total_process = new System.Windows.Forms.ProgressBar();
            this.color_label = new System.Windows.Forms.Label();
            this.lastone_label = new System.Windows.Forms.Label();
            this.finish_button = new System.Windows.Forms.Label();
            this.all_finish_label = new System.Windows.Forms.Label();
            this.Version_label = new System.Windows.Forms.Label();
            this.factory_name = new System.Windows.Forms.Label();
            this.color_info = new System.Windows.Forms.DataGridView();
            this.Color_name_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.done = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unfinish = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Color_name_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Work_id_show = new System.Windows.Forms.Label();
            this.label_ping = new System.Windows.Forms.Label();
            this.color_label_last = new System.Windows.Forms.Label();
            this.lb_error = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.waitTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.user_info)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.color_info)).BeginInit();
            this.SuspendLayout();
            // 
            // waitTable
            // 
            this.waitTable.AllowUserToAddRows = false;
            this.waitTable.AllowUserToDeleteRows = false;
            this.waitTable.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.waitTable.ColumnHeadersHeight = 41;
            this.waitTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.列1工单号,
            this.列1颜色,
            this.列1纹路});
            this.waitTable.Cursor = System.Windows.Forms.Cursors.No;
            this.waitTable.Enabled = false;
            this.waitTable.Location = new System.Drawing.Point(302, 156);
            this.waitTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.waitTable.MultiSelect = false;
            this.waitTable.Name = "waitTable";
            this.waitTable.ReadOnly = true;
            this.waitTable.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.waitTable.RowHeadersVisible = false;
            this.waitTable.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.waitTable.RowTemplate.Height = 27;
            this.waitTable.Size = new System.Drawing.Size(700, 350);
            this.waitTable.TabIndex = 2;
            this.waitTable.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.waitTable_CellContentClick);
            // 
            // 列1工单号
            // 
            this.列1工单号.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.列1工单号.FillWeight = 70F;
            this.列1工单号.HeaderText = "工单号";
            this.列1工单号.Name = "列1工单号";
            this.列1工单号.ReadOnly = true;
            // 
            // 列1颜色
            // 
            this.列1颜色.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.列1颜色.FillWeight = 30F;
            this.列1颜色.HeaderText = "颜色";
            this.列1颜色.Name = "列1颜色";
            this.列1颜色.ReadOnly = true;
            // 
            // 列1纹路
            // 
            this.列1纹路.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.列1纹路.FillWeight = 30F;
            this.列1纹路.HeaderText = "纹路";
            this.列1纹路.Name = "列1纹路";
            this.列1纹路.ReadOnly = true;
            // 
            // labelShow1
            // 
            this.labelShow1.AutoSize = true;
            this.labelShow1.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelShow1.Location = new System.Drawing.Point(398, 92);
            this.labelShow1.Name = "labelShow1";
            this.labelShow1.Size = new System.Drawing.Size(404, 48);
            this.labelShow1.TabIndex = 3;
            this.labelShow1.Text = "请扫描员工卡登陆";
            // 
            // gunReturn
            // 
            this.gunReturn.AcceptsReturn = true;
            this.gunReturn.Location = new System.Drawing.Point(1149, 572);
            this.gunReturn.Name = "gunReturn";
            this.gunReturn.Size = new System.Drawing.Size(101, 24);
            this.gunReturn.TabIndex = 4;
            this.gunReturn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.gunReturn_KeyPress);
            // 
            // user_info
            // 
            this.user_info.AllowUserToAddRows = false;
            this.user_info.AllowUserToDeleteRows = false;
            this.user_info.AllowUserToResizeColumns = false;
            this.user_info.AllowUserToResizeRows = false;
            this.user_info.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.user_info.ColumnHeadersHeight = 25;
            this.user_info.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.User_name,
            this.User_id});
            this.user_info.Enabled = false;
            this.user_info.Location = new System.Drawing.Point(54, -1);
            this.user_info.MultiSelect = false;
            this.user_info.Name = "user_info";
            this.user_info.ReadOnly = true;
            this.user_info.RowHeadersVisible = false;
            this.user_info.RowHeadersWidth = 30;
            this.user_info.RowTemplate.Height = 27;
            this.user_info.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.user_info.Size = new System.Drawing.Size(225, 194);
            this.user_info.TabIndex = 5;
            // 
            // User_name
            // 
            this.User_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.User_name.HeaderText = "员工姓名";
            this.User_name.Name = "User_name";
            this.User_name.ReadOnly = true;
            // 
            // User_id
            // 
            this.User_id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.User_id.FillWeight = 140F;
            this.User_id.HeaderText = "员工号";
            this.User_id.Name = "User_id";
            this.User_id.ReadOnly = true;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // finish_label
            // 
            this.finish_label.AutoSize = true;
            this.finish_label.BackColor = System.Drawing.Color.White;
            this.finish_label.Font = new System.Drawing.Font("宋体", 55F, System.Drawing.FontStyle.Bold);
            this.finish_label.ForeColor = System.Drawing.Color.Red;
            this.finish_label.Location = new System.Drawing.Point(408, 383);
            this.finish_label.Name = "finish_label";
            this.finish_label.Size = new System.Drawing.Size(707, 74);
            this.finish_label.TabIndex = 7;
            this.finish_label.Text = "请扫完成标签返回！";
            this.finish_label.Click += new System.EventHandler(this.finish_label_Click);
            // 
            // state_label
            // 
            this.state_label.AutoSize = true;
            this.state_label.Font = new System.Drawing.Font("宋体", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.state_label.Location = new System.Drawing.Point(115, 231);
            this.state_label.Name = "state_label";
            this.state_label.Size = new System.Drawing.Size(379, 27);
            this.state_label.TabIndex = 8;
            this.state_label.Text = "当前共  条模压工单，余  条.";
            this.state_label.Visible = false;
            // 
            // total_process
            // 
            this.total_process.Location = new System.Drawing.Point(1066, 49);
            this.total_process.Name = "total_process";
            this.total_process.Size = new System.Drawing.Size(200, 23);
            this.total_process.TabIndex = 9;
            this.total_process.Tag = "总进度：";
            this.total_process.Visible = false;
            // 
            // color_label
            // 
            this.color_label.AutoSize = true;
            this.color_label.BackColor = System.Drawing.Color.Black;
            this.color_label.Font = new System.Drawing.Font("宋体", 50.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.color_label.ForeColor = System.Drawing.Color.SpringGreen;
            this.color_label.Location = new System.Drawing.Point(79, 375);
            this.color_label.Name = "color_label";
            this.color_label.Size = new System.Drawing.Size(239, 67);
            this.color_label.TabIndex = 10;
            this.color_label.Text = "label1";
            this.color_label.Click += new System.EventHandler(this.color_label_Click);
            // 
            // lastone_label
            // 
            this.lastone_label.AutoSize = true;
            this.lastone_label.BackColor = System.Drawing.Color.Yellow;
            this.lastone_label.Font = new System.Drawing.Font("宋体", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lastone_label.ForeColor = System.Drawing.Color.Red;
            this.lastone_label.Location = new System.Drawing.Point(279, 567);
            this.lastone_label.Name = "lastone_label";
            this.lastone_label.Size = new System.Drawing.Size(582, 22);
            this.lastone_label.TabIndex = 11;
            this.lastone_label.Text = "这是今日最后一条工单，加工完成后请点击工作完成按钮。";
            this.lastone_label.Visible = false;
            // 
            // finish_button
            // 
            this.finish_button.AutoSize = true;
            this.finish_button.BackColor = System.Drawing.Color.Aqua;
            this.finish_button.Font = new System.Drawing.Font("宋体", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.finish_button.Location = new System.Drawing.Point(566, 607);
            this.finish_button.Name = "finish_button";
            this.finish_button.Size = new System.Drawing.Size(133, 30);
            this.finish_button.TabIndex = 12;
            this.finish_button.Text = "完成工作";
            this.finish_button.Visible = false;
            this.finish_button.Click += new System.EventHandler(this.finish_button_Click);
            // 
            // all_finish_label
            // 
            this.all_finish_label.AutoSize = true;
            this.all_finish_label.Font = new System.Drawing.Font("宋体", 42F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.all_finish_label.Location = new System.Drawing.Point(229, 467);
            this.all_finish_label.Name = "all_finish_label";
            this.all_finish_label.Size = new System.Drawing.Size(584, 56);
            this.all_finish_label.TabIndex = 13;
            this.all_finish_label.Text = "今日工作已全部完成！";
            this.all_finish_label.Visible = false;
            // 
            // Version_label
            // 
            this.Version_label.AutoSize = true;
            this.Version_label.Location = new System.Drawing.Point(12, 626);
            this.Version_label.Name = "Version_label";
            this.Version_label.Size = new System.Drawing.Size(87, 15);
            this.Version_label.TabIndex = 14;
            this.Version_label.Text = "V20181201A";
            this.Version_label.Click += new System.EventHandler(this.Version_label_Click);
            // 
            // factory_name
            // 
            this.factory_name.AutoSize = true;
            this.factory_name.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.factory_name.Location = new System.Drawing.Point(547, 12);
            this.factory_name.Name = "factory_name";
            this.factory_name.Size = new System.Drawing.Size(111, 33);
            this.factory_name.TabIndex = 15;
            this.factory_name.Text = "label1";
            // 
            // color_info
            // 
            this.color_info.AllowUserToAddRows = false;
            this.color_info.AllowUserToDeleteRows = false;
            this.color_info.AllowUserToResizeColumns = false;
            this.color_info.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.color_info.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.color_info.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.color_info.CausesValidation = false;
            this.color_info.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.color_info.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Color_name_1,
            this.done,
            this.Unfinish,
            this.total_num,
            this.Color_name_2,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
            this.color_info.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.color_info.Enabled = false;
            this.color_info.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.color_info.Location = new System.Drawing.Point(283, 12);
            this.color_info.MultiSelect = false;
            this.color_info.Name = "color_info";
            this.color_info.ReadOnly = true;
            this.color_info.RowHeadersVisible = false;
            this.color_info.RowTemplate.Height = 27;
            this.color_info.Size = new System.Drawing.Size(932, 137);
            this.color_info.TabIndex = 16;
            this.color_info.TabStop = false;
            this.color_info.Visible = false;
            this.color_info.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.color_info_CellContentClick);
            // 
            // Color_name_1
            // 
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Color_name_1.DefaultCellStyle = dataGridViewCellStyle2;
            this.Color_name_1.FillWeight = 200F;
            this.Color_name_1.HeaderText = "颜色";
            this.Color_name_1.Name = "Color_name_1";
            this.Color_name_1.ReadOnly = true;
            // 
            // done
            // 
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.done.DefaultCellStyle = dataGridViewCellStyle3;
            this.done.HeaderText = "未完成";
            this.done.Name = "done";
            this.done.ReadOnly = true;
            this.done.Width = 70;
            // 
            // Unfinish
            // 
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Unfinish.DefaultCellStyle = dataGridViewCellStyle4;
            this.Unfinish.HeaderText = "已完成";
            this.Unfinish.Name = "Unfinish";
            this.Unfinish.ReadOnly = true;
            this.Unfinish.Width = 70;
            // 
            // total_num
            // 
            this.total_num.HeaderText = "总计";
            this.total_num.Name = "total_num";
            this.total_num.ReadOnly = true;
            this.total_num.Width = 70;
            // 
            // Color_name_2
            // 
            this.Color_name_2.HeaderText = "颜色";
            this.Color_name_2.Name = "Color_name_2";
            this.Color_name_2.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "未完成";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 70;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "已完成";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 70;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "总计";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 70;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "颜色";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "未完成";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 70;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "已完成";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 70;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "总计";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 70;
            // 
            // Work_id_show
            // 
            this.Work_id_show.AutoSize = true;
            this.Work_id_show.Font = new System.Drawing.Font("宋体", 23F);
            this.Work_id_show.Location = new System.Drawing.Point(488, 65);
            this.Work_id_show.Name = "Work_id_show";
            this.Work_id_show.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Work_id_show.Size = new System.Drawing.Size(298, 31);
            this.Work_id_show.TabIndex = 17;
            this.Work_id_show.Text = "订单号：XXXXXXXXXX";
            this.Work_id_show.Visible = false;
            // 
            // label_ping
            // 
            this.label_ping.AutoSize = true;
            this.label_ping.BackColor = System.Drawing.Color.Crimson;
            this.label_ping.Font = new System.Drawing.Font("宋体", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_ping.Location = new System.Drawing.Point(103, 231);
            this.label_ping.Name = "label_ping";
            this.label_ping.Size = new System.Drawing.Size(810, 97);
            this.label_ping.TabIndex = 18;
            this.label_ping.Text = "未能访问到服务器";
            this.label_ping.Visible = false;
            // 
            // color_label_last
            // 
            this.color_label_last.AutoSize = true;
            this.color_label_last.BackColor = System.Drawing.Color.Black;
            this.color_label_last.Font = new System.Drawing.Font("宋体", 50.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.color_label_last.ForeColor = System.Drawing.Color.Fuchsia;
            this.color_label_last.Location = new System.Drawing.Point(428, 375);
            this.color_label_last.Name = "color_label_last";
            this.color_label_last.Size = new System.Drawing.Size(239, 67);
            this.color_label_last.TabIndex = 19;
            this.color_label_last.Text = "label1";
            // 
            // lb_error
            // 
            this.lb_error.AutoSize = true;
            this.lb_error.BackColor = System.Drawing.Color.Crimson;
            this.lb_error.Font = new System.Drawing.Font("宋体", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_error.Location = new System.Drawing.Point(285, 109);
            this.lb_error.Name = "lb_error";
            this.lb_error.Size = new System.Drawing.Size(762, 97);
            this.lb_error.TabIndex = 20;
            this.lb_error.Text = "该零件尚未喷胶!";
            this.lb_error.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1263, 654);
            this.Controls.Add(this.lb_error);
            this.Controls.Add(this.color_label_last);
            this.Controls.Add(this.label_ping);
            this.Controls.Add(this.Work_id_show);
            this.Controls.Add(this.color_info);
            this.Controls.Add(this.factory_name);
            this.Controls.Add(this.Version_label);
            this.Controls.Add(this.all_finish_label);
            this.Controls.Add(this.finish_button);
            this.Controls.Add(this.lastone_label);
            this.Controls.Add(this.color_label);
            this.Controls.Add(this.total_process);
            this.Controls.Add(this.state_label);
            this.Controls.Add(this.finish_label);
            this.Controls.Add(this.user_info);
            this.Controls.Add(this.gunReturn);
            this.Controls.Add(this.labelShow1);
            this.Controls.Add(this.waitTable);
            this.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Menbran_Contorl";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.waitTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.user_info)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.color_info)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void waitTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape1;
        private DataGridView waitTable;
        private Label labelShow1;
        private TextBox gunReturn;
        private DataGridView user_info;
        private Timer timer1;
        private ContextMenuStrip contextMenuStrip1;
        private Label finish_label;
        private Label state_label;
        private ProgressBar total_process;
        private Label color_label;
        private Label lastone_label;
        private Label finish_button;
        private Label all_finish_label;
        private Label Version_label;
        private DataGridViewTextBoxColumn User_name;
        private DataGridViewTextBoxColumn User_id;
        private Label factory_name;
        private DataGridView color_info;
        private Label Work_id_show;
        private Label label_ping;
        private Label color_label_last;
        private DataGridViewTextBoxColumn 列1工单号;
        private DataGridViewTextBoxColumn 列1颜色;
        private DataGridViewTextBoxColumn 列1纹路;
        private Label lb_error;
        private DataGridViewTextBoxColumn Color_name_1;
        private DataGridViewTextBoxColumn done;
        private DataGridViewTextBoxColumn Unfinish;
        private DataGridViewTextBoxColumn total_num;
        private DataGridViewTextBoxColumn Color_name_2;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column7;
    }
}

