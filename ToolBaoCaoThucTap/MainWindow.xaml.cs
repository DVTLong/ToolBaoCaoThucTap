using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace ToolBaoCaoThucTap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly string weekTitle = "Báo cáo tuần ";
        private int week;
        public MainWindow()
        {
            InitializeComponent();
            DanhSachTuan danhSachTuan = new DanhSachTuan();
            week = danhSachTuan.GetWeekNow();
        }

        private string Trim(string s)
        {
            if (s == null)
            {
                return s;
            }
            s.Trim();
            return s;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txbMSSV.Focus();
            lblWeek.Content = weekTitle + week.ToString();
        }

        [Obsolete]
        private void BtnRun_Click(object sender, RoutedEventArgs e)
        {
            string mssv = Trim(txbMSSV.Text);
            if (string.IsNullOrEmpty(mssv) || string.IsNullOrWhiteSpace(mssv))
            {
                MessageBox.Show("Nhập MSSV");
                return;
            }
            string noiDung = new TextRange(rtbNoiDung.Document.ContentStart, rtbNoiDung.Document.ContentEnd).Text;

            try
            {
                WebDriverWait wait;
                ChromeDriver chromeDriver = new ChromeDriver();
                chromeDriver.Url = "https://ithutech.github.io/thuctapsv/";
                chromeDriver.Navigate();

                var inMSSV = chromeDriver.FindElementById("inMaSV");
                inMSSV.SendKeys(txbMSSV.Text);

                var btnDoSV = chromeDriver.FindElementById("btnDoSV");
                btnDoSV.Click();

                //wait for js complete
                wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(30));
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("btnBaoCao")));

                var btnBaoCao = chromeDriver.FindElementById("btnBaoCao");
                btnBaoCao.Click();

                //wait for js complete
                wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(30));
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("time-report-tuan")));

                var table = chromeDriver.FindElementById("time-report-tuan");

                //wait for js complete
                wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(30));
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector("#time-report-tuan > tbody > tr:nth-child(3) > td:nth-child(4) > button")));
                
                var btnReport = table.FindElement(By.CssSelector("#time-report-tuan > tbody > tr:nth-child(3) > td:nth-child(4) > button"));
                btnReport.Click();

                //wait for js complete
                wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(30));
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//*[@id=\"cke_1_contents\"]/iframe")));

                var iframe = chromeDriver.FindElementByXPath("//*[@id=\"cke_1_contents\"]/iframe");
                chromeDriver.SwitchTo().Frame(iframe);

                var body = chromeDriver.FindElementByClassName("cke_editable");
                body.Click();

                var content = chromeDriver.FindElementByClassName("cke_editable");
                content.SendKeys(noiDung);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void BtnGetData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string filename = "week_" + week.ToString() + ".txt";
                string path = string.Format(@".\Data\{0}", filename);
                string text = File.ReadAllText(path);
                rtbNoiDung.Document.Blocks.Clear();
                rtbNoiDung.Document.Blocks.Add(new Paragraph(new Run(text)));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}
