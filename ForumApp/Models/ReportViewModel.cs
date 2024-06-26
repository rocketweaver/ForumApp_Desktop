﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForumApp.Models
{

        class ReportViewModel
        {
            private Koneksi koneksi;

            public ReportViewModel()
            {
                koneksi = new Koneksi();
            }

            public DataSet Read()
            {
                DataSet ds = new DataSet();
                try
                {
                    koneksi.bukaKoneksi();
                    string query = @"SELECT 
                                    r.*, 
                                    p.description AS post_description, 
                                    p.user_id AS post_owner_id, 
                                    u_post.username AS post_owner_username, 
                                    c.description AS comment_description, 
                                    c.user_id AS comment_owner_id, 
                                    u_comment.username AS comment_owner_username
                                FROM 
                                    reports r
                                LEFT JOIN 
                                    posts p ON r.post_id = p.id_post
                                LEFT JOIN 
                                    comments c ON r.comment_id = c.id_comment
                                LEFT JOIN 
                                    users u_post ON p.user_id = u_post.id_user
                                LEFT JOIN 
                                    users u_comment ON c.user_id = u_comment.id_user";

                    SqlDataAdapter da = new SqlDataAdapter(query, koneksi.con);
                    da.Fill(ds, "reports");

                    return ds;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
                finally
                {
                    koneksi.tutupKoneksi();
                }
            }

        }
}
