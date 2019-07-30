using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ReflexGame
{
    public class UI
    {
        Form1 form;
        Label labelGameOver, labelScore;
        Button buttonReturnToMenu;
        const string scoreStr = "Score: ";
        public int curScore { get; private set; }

        public UI(Form1 form)
        {
            this.form = form;
            curScore = 0;

            labelScore = new Label()
            {
                Text = scoreStr + 0,
                Location = new Point(5, 18),
                Tag = "InGame",
                Visible = true
            };
            form.Controls.Add(labelScore);
        }

        public void AddScore(Rectangle circle)
        {
            double circleArea = Math.PI * (circle.Width / 2) * (circle.Width / 2);
            curScore += (int)circleArea;
            labelScore.Text = scoreStr + curScore;
        }

        public void GameOver()
        {
            labelGameOver = new Label()
            {
                TextAlign = ContentAlignment.MiddleCenter,
                BorderStyle = BorderStyle.None,
                Font = new Font("Arial", 64, FontStyle.Bold),
                Text = "Game Over!" + Environment.NewLine + "Score: " + curScore + Environment.NewLine + "Circles Hit: " + form.circlesHit,
                Size = new Size(600, 400),
                Tag = "GameOver",
                Location = new Point(100, 10),
                Visible = true
            };

            buttonReturnToMenu = new Button()
            {
                Text = "Return to Menu",
                Size = new Size(200, 60),
                Location = new Point(290, 380),
                Tag = "GameOver",
                Visible = true
            };

            buttonReturnToMenu.Click += ButtonReturnToMenu_Click;

            //form.Controls.Add(labelGameOver);
            //form.Controls.Add(buttonReturnToMenu);
            //buttonReturnToMenu.BringToFront();
            form.Invoke(new MethodInvoker(delegate { form.Controls.Add(labelGameOver); }));
            form.Invoke(new MethodInvoker(delegate { form.Controls.Add(buttonReturnToMenu); }));
            form.Invoke(new MethodInvoker(delegate { buttonReturnToMenu.BringToFront(); }));
        }

        private void ButtonReturnToMenu_Click(object sender, EventArgs e)
        {
            form.Size = new Size(271, 318);
            for (int i = 0; i < form.Controls.Count; i++)
            {
                if (form.Controls[i].Tag.ToString() == "Menu")
                {
                    form.Controls[i].Visible = true;
                }
                else
                {
                    form.Controls.Remove(form.Controls[i]);
                    i--;
                }
            }
        }
    }
}
