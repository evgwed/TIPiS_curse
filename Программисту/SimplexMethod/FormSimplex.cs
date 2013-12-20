using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace SimplexMethod
{
    public partial class FormSimplex : Form
    {
        public FormSimplex()
        {
            InitializeComponent();
            //создание помощника для записи решения в файл
            Stream myFile = File.Create("Reshenie.txt");
            TextWriterTraceListener myTextListener = new
               TextWriterTraceListener(myFile);
            Trace.Listeners.Add(myTextListener);
        }

        private int _x_count;//сколько действительных переменных

        private int _ogranich_count;//количество ограничений

        private TCT[] _ogr_arry;//массив конторлов с ограничениями

        private int _fekt_x;//количесвто фективных переменных
        public static String[] textbox_tmp = new String[2];
        private void button_get_ogranich_Click(object sender, EventArgs e)
        {
            //отрисовка формы для задания ограничений
            button_get_ogranich.Enabled = false;
            button_go_method.Enabled = true;
            _x_count = Convert.ToInt32(textBox_count_x.Text);
            _ogranich_count = Convert.ToInt32(textBox_count_ogranich.Text);
            Trace.WriteLine("Кол-во переменных: " + _x_count + "  Кол-во ограничений: " + _ogranich_count);
            _ogr_arry = new TCT[_ogranich_count];
            int hm = 86;
            for (int i = 0; i < _ogranich_count; i++)
            {
                TCT tmp = new TCT(hm, i.ToString());
                tmp.PasteControl(groupBox_ogranich);
                hm += 30;
                if (groupBox_ogranich.Height < hm + 10)
                {
                    groupBox_ogranich.Height += 30;
                }
                _ogr_arry[i] = tmp;
            }
        }
        //определяет сколько будет фиктивных переменных
        private int GetFektPerem(TCT[] sender)
        {
            int ret_val = 0;
            foreach (TCT m in sender)
            {
                if (m.c.SelectedIndex == 0 || m.c.SelectedIndex == 2)
                {
                    ret_val++;
                }
            }
            return ret_val;
        }
        //опеределение количество искуственных переменных
        private int GetIskustvPerem(TCT[] sender)
        {
            int ret_val = 0;
            foreach (TCT m in sender)
            {
                if (m.c.SelectedIndex == 2)
                {
                    ret_val++;
                }
            }
            Trace.WriteLine("Количество искусственных переменных: " + ret_val);
            return ret_val;
        }
        //Кнопка старта поиска решения
        private void button_go_method_Click(object sender, EventArgs e)
        {
            textbox_tmp[0] = textBox_z.Text;
            textbox_tmp[1] = textBox_z_z.Text;
            button_go_method.Enabled = false;
            _fekt_x = GetFektPerem(_ogr_arry);
            func_zele = SimplexMet.CreateTargetFunctionMatrix(_ogr_arry, _ogranich_count, _x_count, textbox_tmp);
            SimplexMet.CreateEquationMatrix(_ogr_arry, _ogranich_count, _x_count, ref usloviya_list, textbox_tmp);
            SimplexMet.AddArtificialVariables(ref usloviya_list, _x_count);
            iskustv_function = SimplexMet.GetArtificialFunctionMatrix(usloviya_list, _x_count);
            simplextable_arry = SimplexMet.FormTableForSimplexMethod(func_zele, usloviya_list, iskustv_function, usloviya_list.Count, _x_count);
            Trace.WriteLine("\nСоставление симплекс таблицы: \n");
            SimplexMet.Writing(simplextable_arry);
            Trace.WriteLine("\nГотовая симплекс таблица с базой:\n");
            SimplexMet.Simplex(ref simplextable_arry);
            Trace.WriteLine("\nРешение найдено!\n");
            string zele = textBox_z.Text + " " + textBox_z_z.Text;
            result_arry = SimplexMet.PoluchenieResheniya(simplextable_arry, func_zele, ref minimum, zele, label_otv);
            TraceMatrix(result_arry);
            Trace.WriteLine("\n\nОтвет: Z(x) = " + minimum);
            // Сохранение все в файл
            Trace.Flush();
        }
        //вывод матрицы на экран
        private void TraceMatrix(object[,] _matrix)
        {
            for (int i = 0; i < _matrix.GetLength(0); i++)
            {
                for (int j = 0; j < _matrix.GetLength(1); j++)
                {
                    Trace.Write(String.Format("{0,16}", _matrix[i, j]));
                }
                Trace.WriteLine("");
            }
        }
        //сплит для строки
        private string[] SpitTextBox(TextBox sender)
        {
            string[] ret_arry = sender.Text.Split(' ');
            return ret_arry;
        }
        //преобразование комбокса в число
        private float ComboxToFloat(ComboBox sender)
        {
            float ret_val = 0F;
            if (sender.SelectedIndex == 0)
            {
                ret_val = -1F;
            }
            else
            {
                if (sender.SelectedIndex == 1)
                {
                    ret_val = 0F;
                }
                else
                {
                    ret_val = 1F;
                }
            }
            return ret_val;
        }
        private object[,] func_zele;
        private List<object[,]> usloviya_list = new List<object[,]>();
        private object[,] iskustv_function;
        private object[,] simplextable_arry;
        private object[,] result_arry;
        private double minimum;

        private void button1_Click(object sender, EventArgs e)
        {

            string[] val = { "8 12 13", "1 1 1", "0 0 1", "0 0 1" };
            string[] val_c = { "200", "13", "4", "5" };
            for (int i = 0; i < _ogr_arry.Length; i++)
            {
                _ogr_arry[i].t1.Text = val[i];
                _ogr_arry[i].t2.Text = val_c[i];
            }
            _ogr_arry[0].c.SelectedIndex = 2;
            _ogr_arry[1].c.SelectedIndex = 1;
            _ogr_arry[2].c.SelectedIndex = 0;
            _ogr_arry[3].c.SelectedIndex = 2;
            textBox_z.Text = "17 28 25";
            textBox_z_z.Text = "0";
        }

        private void buttonv2_Click(object sender, EventArgs e)
        {

            string[] val = { "5 10 7", "1 1 1", "0 1 0", "0 1 0" };
            string[] val_c = { "40", "7", "1", "2" };
            for (int i = 0; i < _ogr_arry.Length; i++)
            {
                _ogr_arry[i].t1.Text = val[i];
                _ogr_arry[i].t2.Text = val_c[i];
            }
            _ogr_arry[0].c.SelectedIndex = 2;
            _ogr_arry[1].c.SelectedIndex = 1;
            _ogr_arry[2].c.SelectedIndex = 0;
            _ogr_arry[3].c.SelectedIndex = 2;
            textBox_z.Text = "21 50 30";
            textBox_z_z.Text = "0";
        }

        private void textBox_count_ogranich_TextChanged(object sender, EventArgs e)
        {
            button_get_ogranich.Enabled = true;
        }

        private void button_v1_Click(object sender, EventArgs e)
        {

            string[] val = { "10 12 17", "1 1 1", "1 0 0", "1 0 0" };
            string[] val_c = { "50", "4", "1", "2" };
            for (int i = 0; i < _ogr_arry.Length; i++)
            {
                _ogr_arry[i].t1.Text = val[i];
                _ogr_arry[i].t2.Text = val_c[i];
            }
            _ogr_arry[0].c.SelectedIndex = 2;
            _ogr_arry[1].c.SelectedIndex = 1;
            _ogr_arry[2].c.SelectedIndex = 0;
            _ogr_arry[3].c.SelectedIndex = 2;
            textBox_z.Text = "15 20 31";
            textBox_z_z.Text = "0";
        }

        private void button_v5_Click(object sender, EventArgs e)
        {

            string[] val = { "5 5 17", "1 1 1", "0 0 1", "0 0 1" };
            string[] val_c = { "150", "15", "2", "3" };
            for (int i = 0; i < _ogr_arry.Length; i++)
            {
                _ogr_arry[i].t1.Text = val[i];
                _ogr_arry[i].t2.Text = val_c[i];
            }
            _ogr_arry[0].c.SelectedIndex = 2;
            _ogr_arry[1].c.SelectedIndex = 1;
            _ogr_arry[2].c.SelectedIndex = 0;
            _ogr_arry[3].c.SelectedIndex = 2;
            textBox_z.Text = "27 23 13";
            textBox_z_z.Text = "0";
        }
    }
    //класс для создания, добавления на форму и забора занчений с формы
    public class TCT
    {
        public TextBox t1;
        public TextBox t2;
        public ComboBox c;
        public TCT()
        {
            this.t1 = null;
            this.t2 = null;
            this.c = null;
        }
        //создание контролов
        public TCT(int h, string pref)
        {
            this.t1 = new TextBox();
            this.t1.Name = "textbox_tmp" + pref;
            this.t1.Width = 156;
            this.t1.Height = 32;
            this.t1.Location = new Point(10, h);
            this.c = new ComboBox();
            this.c.Name = "combox_tmp" + pref;
            this.c.Items.Add(">=");
            this.c.Items.Add("=");
            this.c.Items.Add("=<");
            this.c.Width = 40;
            this.c.Height = 32;
            this.c.Location = new Point(175, h);
            this.t2 = new TextBox();
            this.t2.Name = "textbox_tmp_z" + pref;
            this.t2.Width = 40;
            this.t2.Height = 32;
            this.t2.Location = new Point(220, h);
        }
        //вставка контролов на групбокс
        public void PasteControl(GroupBox gb)
        {
            gb.Controls.Add(this.t1);
            gb.Controls.Add(this.c);
            gb.Controls.Add(this.t2);
        }
    }
}
