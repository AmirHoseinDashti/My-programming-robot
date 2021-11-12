using OneOf.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using ValidationComponents;

namespace My_programming_robot
{
    public partial class Form1 : Form
    {
        private static string Token = "";
        private Thread botThread;
        private Telegram.Bot.TelegramBotClient bot;
        private ReplyKeyboardMarkup mainKeyboardMarkup;
        

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (txtToken.Text == "")
            {
                MessageBox.Show("لطفا توکن ربات خود را وارد کنید", "Error", MessageBoxButtons.OKCancel,MessageBoxIcon.Error);

            }
            else
            {
                Token = txtToken.Text;
                botThread = new Thread(new ThreadStart(runBot));
                botThread.Start();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            mainKeyboardMarkup = new ReplyKeyboardMarkup();
            KeyboardButton[] row1 =
            {
                new KeyboardButton("آموزش دانلود(SQL Express)"), new KeyboardButton("آموزش دانلود(SQL Server)"),new KeyboardButton("آموزش دانلود(Visual Studio)")
            };
            KeyboardButton[] row2 =
            {
                new KeyboardButton("لینک سایت اموزشی زبان برنامه نویسی #C"), new KeyboardButton("منبع اموزشی زبان برنامه نویسی #C"),new KeyboardButton("ارتباط با سازنده ربات")
            };
            KeyboardButton[] row3 =
            {
                new KeyboardButton("درباره ربات" +"\U0001F4A1"), new KeyboardButton("C# درباره زبان برنامه نویسی")
            };

            mainKeyboardMarkup.Keyboard = new KeyboardButton[][]
            {
                row1 ,row2,row3
            };


        }
        void runBot()
        {
            bot = new Telegram.Bot.TelegramBotClient(Token);
            this.Invoke(new Action(() =>
            {
                lblStatus.Text = "Online";
                lblStatus.ForeColor = Color.Green;


            }));
            int offset = 0;
            while (true)
            {
                
                var update = bot.GetUpdatesAsync(offset).Result;

                foreach (var up in update)
                {
                    offset = up.Id + 1;

                    if (up.Message == null)
                        continue;

                    var text = up.Message.Text.ToLower();
                    var from = up.Message.From;
                    var chatId = up.Message.Chat.Id;

                    if (text.Contains("/start"))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine(from.FirstName + " سلام😊");
                        sb.AppendLine("به ربات خوش اومدی ❤");
                        sb.AppendLine("اگه دوست داشتی میتونی از قابلیت هایی ک دارم");
                        sb.AppendLine("استفاده کنی");
                        sb.AppendLine("شاید بتونم کمکت کنم😉😎");
                        bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Default,null,false,false,0,false, mainKeyboardMarkup, default);
                    }
                    else if (text.Contains("درباره ربات" + "\U0001F4A1"))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("سلام 😁❤️");
                        sb.AppendLine("خوشحالم از این اینکه ی سری به من زدی");
                        sb.AppendLine("امیدوارم بتونم کمکت کنم");
                        sb.AppendLine("در رابطه با این قابلیت هایی ک دارم");
                        sb.AppendLine("یا شایدم بتونم  راهنماییت کنم");
                        sb.AppendLine("و تا جایی که بتونم آموزشایی رو ک یاد دارم رو");
                        sb.AppendLine("به اشتراک بزارم داخل این ربات🤖");
                        sb.AppendLine("و یا همچنین آدرس منابع اموزش خوب رو قرار بدم❤️🌹");
                        bot.SendTextMessageAsync(chatId, sb.ToString());

                    }
                    //------------------------------------------------------------------------------

                    else if (text.Contains("ارتباط با سازنده ربات"))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("لطفا انتخاب کنید");

                        ReplyKeyboardMarkup RelationshipKeyboardMarkup = new ReplyKeyboardMarkup();
                        KeyboardButton[] row1 =
                        {
                        new KeyboardButton("آیدی سازنده"), new KeyboardButton("شماره سازنده"), new KeyboardButton("گروه ما"), new KeyboardButton("چنل ما")
                        };
                        KeyboardButton[] row2 =
            {
                new KeyboardButton("بازگشت")
            };



                        RelationshipKeyboardMarkup.Keyboard = new KeyboardButton[][]
                        {
                             row1,row2
                        };

                        bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Default, null, false, false, 0, false, RelationshipKeyboardMarkup, default);

                    }
                    else if (text.Contains("بازگشت"))
                    {
                        bot.SendTextMessageAsync(chatId, "بازگشت به منوی اصلی", ParseMode.Default, null, false, false, 0, false, mainKeyboardMarkup, default);
                    }

                    else if (text.Contains("آیدی سازنده" ))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("درصورت نیاز میتوانید");
                        sb.AppendLine("با ادمین ربات در ارتباط باشید😊😊");
                        List<List<InlineKeyboardButton>> buttons = new List<List<InlineKeyboardButton>>();
                        List<InlineKeyboardButton> row1 = new List<InlineKeyboardButton>();
                        row1.Add(InlineKeyboardButton.WithUrl("آیدی سازنده", "https://t.me/Theres_no_such_thing_as_security"));
                        buttons.Add(row1);
                        InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(buttons);

                        bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Default, null, false, false, 0, false,
                            keyboard, default);

                    }
                    else if (text.Contains("شماره سازنده"))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("دردسترس نیست");
                        bot.SendTextMessageAsync(chatId, sb.ToString());

                    }
                    else if (text.Contains("چنل ما"))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("شما میتوانید درصورت نیاز");
                        sb.AppendLine("عضو چنل ما بشوید");
                        List<List<InlineKeyboardButton>> buttons = new List<List<InlineKeyboardButton>>();
                        List<InlineKeyboardButton> row1 = new List<InlineKeyboardButton>();
                        row1.Add(InlineKeyboardButton.WithUrl("چنل ما", "https://t.me/NewChannel0o0"));
                        buttons.Add(row1);
                        InlineKeyboardMarkup Keyboard = new InlineKeyboardMarkup(buttons);


                        bot.SendTextMessageAsync(chatId, sb.ToString(),ParseMode.Default,null,false,false,0,false,Keyboard,default);

                    }
                    else if (text.Contains("گروه ما"))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("شما میتوانید درصورت نیاز");
                        sb.AppendLine("عضو گروه ما بشوید");
                        List<List<InlineKeyboardButton>> buttons = new List<List<InlineKeyboardButton>>();
                        List<InlineKeyboardButton> row1 = new List<InlineKeyboardButton>();
                        row1.Add(InlineKeyboardButton.WithUrl("گروه ما", "https://t.me/NewGroup0o0"));
                        buttons.Add(row1);
                        InlineKeyboardMarkup Keyboard = new InlineKeyboardMarkup(buttons);
                        bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Default, null, false, false, 0, false, Keyboard, default);
                    }
                    //-----------------------------------------------------------------------------
                    else if (text.Contains("آموزش دانلود(sql express)"))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("شما میتوانید از طریق دکمه های زیر");
                        sb.AppendLine("ابتدا لینک دانلود SQL Express");
                        sb.AppendLine("را دریافت کرده");
                        sb.AppendLine("و سپس آموزش نصب SQL Express");
                        sb.AppendLine("بر روی سیستم عامل ویندوز خود را دریافت کنید 👇👇");
                        List<List<InlineKeyboardButton>> buttons = new List<List<InlineKeyboardButton>>();
                        List<InlineKeyboardButton> row1 = new List<InlineKeyboardButton>();
                        List<InlineKeyboardButton> row2 = new List<InlineKeyboardButton>();
                        List<InlineKeyboardButton> row3 = new List<InlineKeyboardButton>();
                        row1.Add(InlineKeyboardButton.WithUrl("لینک دانلود(SQL Express)",
                            "https://go.microsoft.com/fwlink/?linkid=866658"));
                        row2.Add(InlineKeyboardButton.WithUrl("آموزش نصب(SQL Express)",
                            "https://drive.google.com/drive/folders/1jd3rm9RL4pVlxxR-4kGyfS1GIFeiqFdt?usp=sharing"));
                        row3.Add(InlineKeyboardButton.WithUrl("آموزش نصب(SSMS)", "https://aka.ms/ssmsfullsetup"));
                        buttons.Add(row1);
                        buttons.Add(row2);
                        buttons.Add(row3);
                        InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(buttons);
                        bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Default, null, false, false, 0, false,
                            keyboard, default);
                    }
                    //------------------------------------------------------------------------------------------------

                    else if (text.Contains("آموزش دانلود(sql server)"))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("شما میتوانید از طریق دکمه های زیر");
                        sb.AppendLine("ابتدا لینک دانلود SQL Server");
                        sb.AppendLine("را دریافت کرده");
                        sb.AppendLine("و سپس آموزش نصب SQL Server");
                        sb.AppendLine("بر روی سیستم عامل ویندوز خود را دریافت کنید 👇👇");
                        List<List<InlineKeyboardButton>> buttons = new List<List<InlineKeyboardButton>>();
                        List<InlineKeyboardButton> row1 = new List<InlineKeyboardButton>();
                        List<InlineKeyboardButton> row2 = new List<InlineKeyboardButton>();
                        List<InlineKeyboardButton> row3 = new List<InlineKeyboardButton>();
                        row1.Add(InlineKeyboardButton.WithUrl("لینک دانلود(SQL Server)",
                            "https://go.microsoft.com/fwlink/?linkid=866662"));
                        row2.Add(InlineKeyboardButton.WithUrl("آموزش نصب(SQL Server)",
                            "https://drive.google.com/drive/folders/1hlMpoVx3E4QUMPaNU9HdsTXQVVIAxdYL?usp=sharing"));
                        row3.Add(InlineKeyboardButton.WithUrl("آموزش نصب(SSMS)", "https://aka.ms/ssmsfullsetup"));
                        buttons.Add(row1);
                        buttons.Add(row2);
                        buttons.Add(row3);
                        InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(buttons);


                        bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Default, null, false, false, 0, false, keyboard, default);
                    }
                    //-------------------------------------------------------------------------------------------------------------
                    else if (text.Contains("c# درباره زبان برنامه نویسی"))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("درباره سی شارپ :");
                        sb.AppendLine("بدون شک یکی از محبوب ترین و پرکاربرد ترین زبان های برنامه نویسی حال حاضر دنیا سی شارپ نام دارد و بر اساس آخرین تحقیقات صورت گرفته این زبان");
                        sb.AppendLine("جزو 5 زبان برنامه نویسی برتر در دنیا می باشد، که همچنین بازار کار بسیار خوبی در ایران دارد. از زبان برنامه نویسی C# می توان برای ساخت برنامه های");
                        sb.AppendLine("تحت ویندوز (دسکتاپ)، برنامه های تحت وب،Web service  ها، برنامه های موبایل و بازی ها استفاده کرد.");
                        sb.AppendLine("برای توسعه برنامه های ویندوز از طریق زبان سی شارپ می توان از پلتفرم های WinForms،WPF  و UWP استفاده کرد. حتی با استفاده از زبان سی شارپ و");
                        sb.AppendLine("پلتفرم هایی مانند Xamarin و UWP می ‌توان برای اندروید،  iOS و ویندوزفون‌ ها برنامه ایجاد کرد. همچنین فریمورک های ASP.NET MVC  و ASP.NET");
                        sb.AppendLine("Core دو تکنولوژی برای طراحی و ایجاد برنامه های تحت وب مدرن هستند که امروزه در دنیای برنامه نویسی بسیار پرکاربرد و پر آوازه می باشند. بنابراین اگر");
                        sb.AppendLine("تسلط خوبی به زبان برنامه نویسی سی شارپ داشته باشید، شما آمادگی کافی برای شروع یادگیری فریمورک قدرتمند ASP.NET Core را نیز خواهید داشت.");
                        bot.SendTextMessageAsync(chatId, sb.ToString());

                    }
                    //--------------------------------------------------------------------------------------------------------------
                    else if (text.Contains("آموزش دانلود(visual studio)"))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("شما میتوانید از طریق دکمه های زیر");
                        sb.AppendLine("ابتدا لینک دانلود Visual Studio");
                        sb.AppendLine("را دریافت کرده");
                        sb.AppendLine("و سپس آموزش نصب Visual Studio");
                        sb.AppendLine("بر روی سیستم عامل ویندوز خود را دریافت کنید 👇👇");
                        List<List<InlineKeyboardButton>> buttons = new List<List<InlineKeyboardButton>>();
                        List<InlineKeyboardButton> row1 = new List<InlineKeyboardButton>();
                        List<InlineKeyboardButton> row2 = new List<InlineKeyboardButton>();
                        row1.Add(InlineKeyboardButton.WithUrl("لینک دانلود(Visual Studio)",
                            "https://drive.google.com/drive/folders/1S_b1mleXj0JuZgPaFuTXe0vzkPCAFC4I?usp=sharing"));
                        row2.Add(InlineKeyboardButton.WithUrl("آموزش نصب(Visual Studio)",
                            "https://drive.google.com/drive/folders/1S_b1mleXj0JuZgPaFuTXe0vzkPCAFC4I?usp=sharing"));
                        buttons.Add(row1);
                        buttons.Add(row2);
                        InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(buttons);


                        bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Default, null, false, false, 0, false, keyboard, default);
                    }
                    //------------------------------------------------------------------------------------------------------------------------------------
                    else if (text.Contains("منبع اموزشی زبان برنامه نویسی #c"))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("تو میتونی از طریق دکمه های زیر");
                        sb.AppendLine("فیلم های آموزش زبان برنامه نویسی سی شارپ رو");
                        sb.AppendLine("یاد بگیری بهتره اول وقتتو واسه  دوره مبتدی بزاری ✅");
                        sb.AppendLine("بعد اگه خوشت اومد میتونی دوره پیشرفته روهم ادامه بدی😊😊");
                        List<List<InlineKeyboardButton>> buttons = new List<List<InlineKeyboardButton>>();
                        List<InlineKeyboardButton> row1 = new List<InlineKeyboardButton>();
                        List<InlineKeyboardButton> row2 = new List<InlineKeyboardButton>();
                        row1.Add(InlineKeyboardButton.WithUrl("آموزش مبتدی سی شارپ",
                            "https://toplearn.com/c/gJY"));
                        row2.Add(InlineKeyboardButton.WithUrl("آموزش پیشرفته سی شارپ",
                            "https://toplearn.com/c/mZO"));
                        buttons.Add(row1);
                        buttons.Add(row2);
                        InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(buttons);

                        bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Default, null, false, false, 0, false, keyboard, default);
                    }
                    else if (text.Contains("لینک سایت اموزشی زبان برنامه نویسی #c"))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("شما میتوانید");
                        sb.AppendLine("از طریق دکمه زیر");
                        sb.AppendLine("به سایت آموزشی سی شارپ هدایت شوید👇😐");
                        List<List<InlineKeyboardButton>> buttons = new List<List<InlineKeyboardButton>>();
                        List<InlineKeyboardButton> row1 = new List<InlineKeyboardButton>();
                        row1.Add(InlineKeyboardButton.WithUrl("سایت آموزش سی شارپ",
                            "https://www.w3schools.com/cs/index.php"));
                        buttons.Add(row1);
                        InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(buttons);

                        bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Default, null, false, false, 0, false, keyboard, default);

                    }


                    dgReport.Invoke(new Action(() =>
                    {
                        dgReport.Rows.Add(chatId, from.Username, text, up.Message.MessageId,
                            up.Message.Date.ToString("yyyy/MM/dd - HH:mm"));
                    }));
                }
            }



        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            botThread.Abort();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if(txtMessage.Text == "")
            {
                MessageBox.Show("لطفا متنی را وارد کنید", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            }
            else
            {

                if (dgReport.CurrentRow != null)
                {
                    int chatId = int.Parse(dgReport.CurrentRow.Cells[0].Value.ToString());
                    bot.SendTextMessageAsync(chatId, txtMessage.Text, ParseMode.Html);
                    txtMessage.Text = "";
                }
            }
        }


        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                txtFilePach.Text = openFile.FileName;
            }
        }

        private void btnPhoto_Click(object sender, EventArgs e)
        {
            if (txtFilePach.Text == "")
            {
                MessageBox.Show("لطفا تصویری را انتخاب کنید", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            }
            else
            {

                if (dgReport.CurrentRow != null)
                {
                    int chatId = int.Parse(dgReport.CurrentRow.Cells[0].Value.ToString());
                    FileStream imageFile = System.IO.File.Open(txtFilePach.Text, FileMode.Open);
                    bot.SendPhotoAsync(chatId, imageFile, txtMessage.Text);
                }
            }
        }

        private void btnVideo_Click(object sender, EventArgs e)
        {
            if (txtFilePach.Text == "")
            {
                MessageBox.Show("لطفا فیلمی را انتخاب کنید", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            }
            else
            {

                if (dgReport.CurrentRow != null)
                {
                    int chatId = int.Parse(dgReport.CurrentRow.Cells[0].Value.ToString());
                    FileStream videoFile = System.IO.File.Open(txtFilePach.Text, FileMode.Open);
                    bot.SendPhotoAsync(chatId, videoFile, txtMessage.Text);
                }
            }

        }

        private void btnSendText_Click(object sender, EventArgs e)
        {
            if (txtChannel.Text == "")
            {
                MessageBox.Show("لطفا آیدی کانال را با'@'وارد کنید", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            }
            else
            {
                if (txtMessage.Text == "")
                {
                    MessageBox.Show("لطفا متنی را وارد کنید و دوباره کلید 'Send Text'را بزنید", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else
                {

                    bot.SendTextMessageAsync(txtChannel.Text, txtMessage.Text, ParseMode.Html);
                }
            }
        }

        private void btnSendPhoto_Click(object sender, EventArgs e)
        {
            if (txtChannel.Text == "")
            {
                MessageBox.Show("لطفا آیدی کانال را با'@'وارد کنید", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            }
            else
            {
                if(txtFilePach.Text == "")
                {
                    MessageBox.Show("'...'لطفا تصویری را انتخاب کنیداز قسمت  ", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else
                {

                    FileStream imageFile = System.IO.File.Open(txtFilePach.Text, FileMode.Open);

                    bot.SendPhotoAsync(txtChannel.Text, imageFile, txtMessage.Text);
                }
            }
        }

        private void btnSendVideo_Click(object sender, EventArgs e)
        {
            if (txtChannel.Text == "")
            {
                MessageBox.Show("لطفا آیدی کانال را با'@'وارد کنید", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            }
            else
            {
                if (txtFilePach.Text == "")
                {
                    MessageBox.Show("'...'لطفا ویدیئویی را انتخاب کنیداز قسمت  ", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else
                {

                    FileStream videoFile = System.IO.File.Open(txtFilePach.Text, FileMode.Open);

                    bot.SendVideoAsync(txtChannel.Text, videoFile);
                }

            }
        }
    }
}
