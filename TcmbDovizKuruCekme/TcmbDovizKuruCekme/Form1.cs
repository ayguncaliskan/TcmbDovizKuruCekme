using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace TcmbDovizKuruCekme
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //Form Yüklediğinde label8in görünmesini istemedim bu yüzde visibleı false yaptım.
        private void Form1_Load(object sender, EventArgs e)
        {
            label8.Visible = false;
        }            
        private void btnVeri_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Visible = true;
                dateTimePicker1.Format = DateTimePickerFormat.Custom;           
                dateTimePicker1.CustomFormat = "yyyyMM/ddMMyyyy";//DatetimePickerin değerinini tcmbnin tarih formatına göre ayarladım
                string zaman = dateTimePicker1.Text;
                string exchangeRate = "http://www.tcmb.gov.tr/kurlar/";
                string son = ".xml";
                string toplu = exchangeRate + zaman + son;// Verileri birleştirek xml linkini oluşturdum.
                DataSet dataSet = new DataSet(); //Gelicek olan data buraya atılacaktır
                dataSet.ReadXml(toplu);  //Belirtilen dosyayı kullanarak XML şemasını ve verilerini içinde DataSet okur.         
                dataGridView1.DataSource = dataSet.Tables[1];//xml linkinde tüm alanları datagridviewe çektim.              
                label8.Visible = false;
            }
            catch (Exception)
            {
                dataGridView1.Visible = false;
                label8.Visible = true;
                label8.Text = "Veriler alınamadı bu tarihte veri yayınlanmamıştır!!";               
            }
        }
        //Burada Cellclick eventi kullanılmıştır seçilen satırdaki istediğimiz veriyi textbotlara yazdırmaya yarar.
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }
        //Burada seçilen döviz kurunun miktarı girilerek hesaplanıp textboxa yazdırılır.
        private void BtnHesap_Click(object sender, EventArgs e)
        {
            try
            {
                decimal kur = Convert.ToDecimal(textBox5.Text);
                decimal miktar = Convert.ToDecimal(textBox6.Text);
                decimal hesap = miktar * kur;
                textBox4.Text = hesap.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Önce Tarih Seçip Verileri Getiriniz ve Miktarı giriniz!!", "Bilgilendirme Penceresi", MessageBoxButtons.OK);

            }            
        }
    }
}
