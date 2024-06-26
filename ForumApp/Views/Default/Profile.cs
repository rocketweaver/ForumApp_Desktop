﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForumApp
{
    public partial class Profile : Form
    {
        int id;
        public Profile(int id)
        {
            this.id = id;

            InitializeComponent();
        }

        private void LoadUserPosts()
        {
            flowLayoutPosts.Controls.Clear();

            try
            {
                DataSet panelData;

                PostsModel posts = new PostsModel();
                posts.userId = id.ToString();
                panelData = posts.ReadByUserId();

                foreach (DataRow row in panelData.Tables["posts"].Rows)
                {
                    var panelId = row["id_post"].ToString();
                    var panelInfo = new { title = row["title"].ToString(), date = (DateTime)row["post_date"] };

                    var panel = new Panel
                    {
                        Name = panelId,
                        Tag = panelId,
                        Size = new Size(flowLayoutPosts.Width - 39, 40),
                        Margin = new Padding(10, 20, 5, 20),
                        BackColor = Color.White,
                        BorderStyle = BorderStyle.FixedSingle,
                        Cursor = Cursors.Hand,
                    };

                    var titleLabel = new Label
                    {
                        Text = panelInfo.title,
                        Location = new Point(10, 10),
                        Font = new Font("Segoe UI", 8, FontStyle.Bold),
                        ForeColor = Color.Black,
                        AutoSize = true
                    };
                    panel.Controls.Add(titleLabel);

                    var dateLabel = new Label
                    {
                        Text = panelInfo.date.ToString("yyyy-MM-dd"),
                        Location = new Point(panel.Width - 110, 10),
                        Font = new Font("Segoe UI", 8),
                        TextAlign = ContentAlignment.MiddleRight
                    };
                    panel.Controls.Add(dateLabel);

                    panel.Click += post_Click;

                    flowLayoutPosts.Controls.Add(panel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadUserShares()
        {
            flowLayoutShares.Controls.Clear();

            try
            {
                DataSet panelData;

                PostsModel posts = new PostsModel();
                posts.userId = id.ToString();
                panelData = posts.ReadByShare();

                foreach (DataRow row in panelData.Tables["posts"].Rows)
                {
                    var panelId = row["id_post"].ToString();
                    var panelInfo = new { title = row["title"].ToString(), date = (DateTime)row["post_date"] };

                    var panel = new Panel
                    {
                        Name = panelId,
                        Tag = panelId,
                        Size = new Size(flowLayoutPosts.Width - 39, 40),
                        Margin = new Padding(10, 20, 5, 20),
                        BackColor = Color.White,
                        BorderStyle = BorderStyle.FixedSingle,
                        Cursor = Cursors.Hand,
                    };

                    var titleLabel = new Label
                    {
                        Text = panelInfo.title,
                        Location = new Point(10, 10),
                        Font = new Font("Segoe UI", 8, FontStyle.Bold),
                        ForeColor = Color.Black,
                        AutoSize = true
                    };
                    panel.Controls.Add(titleLabel);

                    var dateLabel = new Label
                    {
                        Text = panelInfo.date.ToString("yyyy-MM-dd"),
                        Location = new Point(panel.Width - 110, 10),
                        Font = new Font("Segoe UI", 8),
                        TextAlign = ContentAlignment.MiddleRight
                    };
                    panel.Controls.Add(dateLabel);

                    panel.Click += post_Click;

                    flowLayoutShares.Controls.Add(panel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void post_Click(object sender, EventArgs e)
        {
            var clickedPanel = (Panel)sender;
            string panelId = clickedPanel.Tag.ToString();

            PostsModel posts = new PostsModel();
            posts.id = panelId;
            DataRow postById = posts.ReadById();

            if (postById != null)
            {
                this.Hide();
                Posts postForm = new Posts(panelId);
                postForm.Closed += (s, args) => this.Close();
                postForm.Show();
            }
            else
            {
                MessageBox.Show("Related post doesn't exist.");
            }
        }

        private void Profile_Load(object sender, EventArgs e)
        {
            loggedinUserLabel.Text = UsersModel.Username;

            UsersModel user = new UsersModel();
            user.id = id;
            DataRow userById = user.ReadById();

            if(userById != null)
            {
                usernameLabel.Text = userById["username"].ToString();

                LoadUserPosts();
                LoadUserShares();
            } else
            {
                MessageBox.Show("No user related exists.");
            }
        }

        private void homeLabel_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home home = new Home();
            home.Closed += (s, args) => this.Close();
            home.Show();
        }

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            UsersModel.SetUsers("", "");

            this.Hide();

            LoginForm login = new LoginForm();
            login.Closed += (s, args) => this.Close();
            login.Show();
        }

        private void loggedinUserLabel_Click(object sender, EventArgs e)
        {
            this.Hide();

            Profile profile = new Profile(UsersModel.UserId);
            profile.Closed += (s, args) => this.Close();
            profile.Show();
        }

        private void homeLabel_Click_1(object sender, EventArgs e)
        {
            this.Hide();

            Home home = new Home();
            home.Closed += (s, args) => this.Close();
            home.Show();
        }

        private string PromptPin()
        {
            using (var form = new Form())
            using (var pinLabel = new Label())
            using (var pinTxt = new TextBox())
            using (var submitButton = new Button())
            {
                pinLabel.Text = "PIN:";
                pinLabel.Font = new Font("Segoe UI", 9);

                pinTxt.UseSystemPasswordChar = true;
                pinTxt.Size = new Size(200, 20);
                pinTxt.BackColor = Color.WhiteSmoke;

                submitButton.Text = "Submit";
                submitButton.Size = new Size(80, 30);
                submitButton.Font = new Font("Segoe UI", 9);
                submitButton.BackColor = Color.SteelBlue;
                submitButton.ForeColor = Color.White;
                submitButton.FlatStyle = FlatStyle.Flat;
                submitButton.FlatAppearance.BorderSize = 0;
                submitButton.DialogResult = DialogResult.OK;
                submitButton.Cursor = Cursors.Hand;

                form.Text = "Confirm PIN";
                form.Size = new Size(300, 200);
                form.FormBorderStyle = FormBorderStyle.FixedSingle;
                form.StartPosition = FormStartPosition.CenterParent;
                form.AcceptButton = submitButton;
                form.CancelButton = submitButton;
                form.Controls.AddRange(new Control[] { pinLabel, pinTxt, submitButton });
                form.BackColor = Color.White;

                int centerX = form.ClientSize.Width / 2;
                int centerY = form.ClientSize.Height / 2;
                pinLabel.Location = new Point(40, centerY - 40);
                pinTxt.Location = new Point(centerX - pinTxt.Width / 2, centerY - 20);
                submitButton.Location = new Point(centerX - submitButton.Width / 2, centerY + 15);

                submitButton.Click += (sender, e) =>
                {
                    if (string.IsNullOrEmpty(pinTxt.Text))
                    {
                        MessageBox.Show("Please enter your PIN.");
                        form.DialogResult = DialogResult.None;
                    }
                };

                if (form.ShowDialog() == DialogResult.OK)
                {
                    return pinTxt.Text;
                }
                else
                {
                    return null;
                }
            }
        }

        private void usernameLabel_Click(object sender, EventArgs e)
        {
            string pin = PromptPin();
            UsersModel user = new UsersModel();

            if (!string.IsNullOrEmpty(pin))
            {
                if (UsersModel.CheckPin(id, pin))
                {
                    user.id = id;

                    DataRow userById = user.ReadById();

                    if (userById != null)
                    {
                        this.Hide();

                        EditProfile editProfile = new EditProfile(id);
                        editProfile.Closed += (s, args) => this.Close();
                        editProfile.Show();
                    }
                    else
                    {
                        MessageBox.Show("No user related exists.");
                    }
                } else
                {
                    MessageBox.Show("Incorrect PIN.");
                }
            }
        }
    }
}
