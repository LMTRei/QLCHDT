using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;

namespace QLCHDT
{
    class Functions
    {
        //Khai báo đối tượng kết nối  
        public static SqlConnection Con;
        string strcon = @"Data Source=LAPTOP-VDLVVBQK\LOCAL;Initial Catalog=QLCHDD;Integrated Security=True";

        public void Connect()
        {
            
            Con = new SqlConnection(strcon);
            if (Con.State == ConnectionState.Closed)
            {  //Mở kết nối
                Con.Open();
                MessageBox.Show("Kết nối DB thành công");
            }
               

            ////Kiểm tra kết nối
            if (Con.State == ConnectionState.Open)
                MessageBox.Show("Kết nối DB thành công");
            else MessageBox.Show("Không thể kết nối với DB");
        }
        public static void Disconnect()
        {
            if (Con.State == ConnectionState.Open)
            {
                //Đóng kết nối
                Con.Close();

                //Giải phóng tài nguyên
                Con.Dispose();
                Con = null;

                //Kiểm tra kết nối
                //MessageBox.Show("Đóng Kết nối DB thành công");
            }
        }
        public static DataTable GetDataToTable(string sql) //Lấy dữ liệu đổ vào bảng
        {
            SqlDataAdapter dap = new SqlDataAdapter();
            dap.SelectCommand = new SqlCommand();

            //Kết nối cơ sở dữ liệu
            dap.SelectCommand.Connection = Functions.Con;
            dap.SelectCommand.CommandText = sql;

            DataTable table = new DataTable();
            dap.Fill(table);
            return table;
        }
        public static bool CheckKey(string sql) // kiểm tra xem có trùng khóa hay không
        {
            SqlDataAdapter dap = new SqlDataAdapter(sql,Con);
            DataTable table = new DataTable();
            dap.Fill(table);
            if (table.Rows.Count > 0)
                return true;
            else return false;
        }

        public static void RunSQL(string sql) // chạy câu lệnh sql
        {
            SqlCommand cmd = new SqlCommand();

            //Gán kết nối
            cmd.Connection = Con;

            //Gán lệnh SQL
            cmd.CommandText = sql;

            //Thực hiện câu lệnh SQL
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            //Giải phóng bộ nhớ
            cmd.Dispose();
            cmd = null;
        }
        public static void FillCombo(string sql, ComboBox cbo, string ma, string ten) // đổ dữ liệu vào comboBox
        {
            SqlDataAdapter dap = new SqlDataAdapter(sql, Con);
            DataTable table = new DataTable();
            dap.Fill(table);
            cbo.DataSource = table;
            cbo.ValueMember = ma;
            cbo.DisplayMember = ten;
        }

        public static string GetFieldValues(string sql) // lấy dữ liệu từ câu lệnh sql
        {
            string ma = "";
            SqlCommand cmd = new SqlCommand(sql, Con);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
                ma = reader.GetValue(0).ToString();
            reader.Close();
            return ma;
        }

        // chuyển dd/mm/yy -> mm/dd/yy. Vì mm/dd/yy khi lưu vào csdl sẽ tự chuyển thành 
        // dd/mm/yy , khi ta lấy ra nó sẽ có dạng dd/mm/yy 
        // tiện cho việc lấy dữ liệu ra và gán vào textBox luôn
        public static string ConvertDateTime(string date)
        {
            string[] elements = date.Split('-');
            string dt = string.Format("{0}/{1}/{2}", elements[1], elements[0], elements[2]);
            return dt;
        }

        private static string get_imageName(string linkanh) // lấy tên ảnh
        {
            string tenanh = "";
            char ktdung = '\\';
            int d = 0;

            // lấy vị trí bắt đầu của tên ảnh
            for (int i = linkanh.Length - 1; i >= 0; i--)
            {
                if (linkanh[i] == ktdung) break;
                d++;
            }

            for (int i = linkanh.Length - d; i < linkanh.Length; i++)
                tenanh += linkanh[i];
            return tenanh;
        }

        public static void update_imageLink()
        {
            DataTable tbl = GetDataToTable("SELECT * FROM DIENTHOAI");
            string linkanh;
            string madt;
            string path = System.IO.Directory.GetCurrentDirectory();

            foreach (DataRow row in tbl.Rows)
            {
                linkanh = row["ANH"].ToString();
                madt = row["MADT"].ToString();

                string linkanhmoi = path + "\\HinhDienThoai\\" + get_imageName(linkanh);
                string sql = "UPDATE DIENTHOAI SET ANH = N'" + linkanhmoi + "' WHERE MADT = '" + madt + "'";
                Functions.RunSQL(sql);
            }

        }
    }
}
