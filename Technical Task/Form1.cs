using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace Technical_Task
{
    public partial class Form1 : Form
    {

        private string startupPath = Environment.CurrentDirectory;
        List<Bill> bills = new List<Bill>();
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Add("A");
            comboBox1.Items.Add("B");
            comboBox1.Items.Add("C");
            comboBox1.Items.Add("D");

            startupPath = startupPath.Substring(0, startupPath.Length - 10);
            DataGridViewComboBoxColumn ItemCmb = new DataGridViewComboBoxColumn();
            ItemCmb.HeaderText = "Item";
            ItemCmb.Items.Add("A");
            ItemCmb.Items.Add("B");
            ItemCmb.Items.Add("C");
            ItemCmb.Items.Add("D");
            dataGridView1.Columns.Add(ItemCmb);

            DataGridViewComboBoxColumn UnitCmb = new DataGridViewComboBoxColumn();
            UnitCmb.HeaderText = "Unit";
            UnitCmb.Items.Add("A");
            UnitCmb.Items.Add("B");
            UnitCmb.Items.Add("C");
            UnitCmb.Items.Add("D");
            dataGridView1.Columns.Add(UnitCmb);

            dataGridView1.ColumnCount = 7;

            dataGridView1.Columns[2].Name = "Price";
            dataGridView1.Columns[3].Name = "Qty";
            dataGridView1.Columns[4].Name = "Total";
            dataGridView1.Columns[5].Name = "Discount";
            dataGridView1.Columns[6].Name = "Net";

            dataGridView1.Columns[4].ReadOnly = true;
            dataGridView1.Columns[6].ReadOnly = true;

        }

        void save()
        {
            // get lines from the text file
            string[] lines = File.ReadAllLines(startupPath + @"\Text.txt");
            string[] values;


            for (int i = 0; i < lines.Length; i++)
            {
                values = lines[i].ToString().Split('|');
                string[] row = new string[7];

                for (int j = 0; j < 7; j++)
                {

                    if (j < 7)
                    {
                        row[j] = values[j].Trim();
                    }

                }

                Bill bill = new Bill();
                bill.Item = values[0].Trim();   //0
                bill.Unit = values[1].Trim();  //1
                bill.Price = values[2].Trim();  //2
                bill.Qty = values[3].Trim();  //3
                bill.Total = values[4].Trim();  //4
                bill.Discount = values[5].Trim(); //5
                bill.Net = values[6].Trim();   //6
                bill.Taxes = values[7].Trim(); //7
                bill.Store = values[8].Trim();//8
                bill.Invoice_Date = Convert.ToDateTime(values[9].Trim());//9
                bill.Invoice_No = values[10].Trim(); //10

                bills.Add(bill);
                dataGridView1.Rows.Add(row);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            string startupPath = Environment.CurrentDirectory;
            startupPath = startupPath.Substring(0, startupPath.Length - 10);
            MessageBox.Show(startupPath);
            TextWriter writer = new StreamWriter(startupPath + @"\Text.txt");
            try
            {
                for (int i = 0; i < bills.Count; i++)
                {
                    writer.Write("\t" + bills[i].Item + "\t" + "|");    //0
                    writer.Write("\t" + bills[i].Unit + "\t" + "|");    //1
                    writer.Write("\t" + bills[i].Price + "\t" + "|");   //2
                    writer.Write("\t" + bills[i].Qty + "\t" + "|");     //3
                    writer.Write("\t" + bills[i].Total + "\t" + "|");   //4
                    writer.Write("\t" + bills[i].Discount + "\t" + "|");//5
                    writer.Write("\t" + bills[i].Net + "\t" + "|");     //6

                    writer.Write("\t" + bills[i].Taxes + "\t" + "|");
                    writer.Write("\t" + bills[i].Store + "\t" + "|");
                    writer.Write("\t" + bills[i].Invoice_Date + "\t" + "|");
                    writer.Write("\t" + bills[i].Invoice_No + "\t" + "|");

                    writer.Write("\t" + bills[i].id + "\t" + "|");
                    writer.Write("\n");


                }
            }
            catch { }
            writer.Close();
            MessageBox.Show("Data Exported");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.save();
        }



        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            int x = dataGridView1.CurrentRow.Index;

            if (bills.Count-1 >= x)
            {
               

                textBox2.Text = bills[x].Total;


                textBox4.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();

                textBox3.Text = bills[x].Taxes;

                comboBox1.Text = bills[x].Store;

                dateTimePicker1.Value = bills[x].Invoice_Date;

                textBox1.Text = bills[x].Invoice_No;
            }
            else if (bills.Count <= x)
            {

                textBox2.Text = textBox4.Text = textBox3.Text = comboBox1.Text = textBox1.Text = "";
            }


        }

        private void Add_Click(object sender, EventArgs e)
        {
            Bill bill = new Bill();
            bill.id = dataGridView1.CurrentRow.Index;

            bill.Item = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            bill.Unit = dataGridView1.CurrentRow.Cells[1].Value.ToString();

            bill.Price = dataGridView1.CurrentRow.Cells[2].Value.ToString();

            bill.Qty = dataGridView1.CurrentRow.Cells[3].Value.ToString();

            dataGridView1.CurrentRow.Cells[4].Value = bill.Total = textBox2.Text;

            bill.Discount = dataGridView1.CurrentRow.Cells[5].Value.ToString();

            dataGridView1.CurrentRow.Cells[6].Value = bill.Net = textBox4.Text;

            bill.Taxes = textBox3.Text;

            bill.Store = comboBox1.Text;

            bill.Invoice_Date = dateTimePicker1.Value;

            bill.Invoice_No = textBox1.Text;

            if (!bills.Exists(s => s.id == dataGridView1.CurrentRow.Index))
            {
                bills.Add(bill);
            }
            else
            {
                bills[dataGridView1.CurrentRow.Index] = bill;
            }

            textBox2.Text = "";

            textBox4.Text= "";

            MessageBox.Show(bills[0].Unit.ToString());

        }

        private void Save_Click(object sender, EventArgs e)
        {
            this.save();
        }

        private void Delete_Click(object sender, EventArgs e)
        {

        }

        


    }
}

