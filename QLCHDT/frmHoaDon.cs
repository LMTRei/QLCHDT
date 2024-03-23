using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCHDT
{
    public partial class HoaDon : Form
    {
        string strcon = @"Data Source=LAPTOP-VDLVVBQK\LOCAL;Initial Catalog=QLCHDD;Integrated Security=True";
        SqlConnection sqlcon = null;
        public HoaDon()
        {
            InitializeComponent();
        }
        
        DataTable tblHD;
        Class.hoadon Obj_HD = new Class.hoadon();
        private static bool mode_find_HD = false;

        public void Connect()
        {

            sqlcon = new SqlConnection(strcon);
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
                MessageBox.Show("Ket noi thanh cong");
            }


            ////Kiểm tra kết nối
            if (sqlcon.State == ConnectionState.Open)
                MessageBox.Show("Kết nối DB thành công");
            else MessageBox.Show("Không thể kết nối với DB");
        }

        private void setFont_DH() // set Font cho các textBox 
        {
            txtBox_madonhang_HD.Font = new Font("Time New Roman", 12);
            txtBox_mahang_HD.Font = new Font("Time New Roman", 12);
            txtBox_madt_HD.Font = new Font("Time New Roman", 12);
            txtBox_makh_HD.Font = new Font("Time New Roman", 12);
            txtBox_manv_HD.Font = new Font("Time New Roman", 12);
            dTP_ngayban_HD.Font = new Font("Time New Roman", 12);
            txtBox_tongtien_HD.Font = new Font("Time New Roman", 12);
            cbBox_thang_HD.Font = new Font("Time New Roman", 12);
            txtBox_slban_HD.Font = new Font("Time New Roman", 12);
            txtBox_giaban_HD.Font = new Font("Time New Roman", 12);
        }

        private void ResetValues_HD() // reset giá trị cho các mục 
        {
            if (!mode_find_HD)
                cbBox_thang_HD.Text = "";

            txtBox_madonhang_HD.Text = "";
            txtBox_mahang_HD.Text = "";
            txtBox_madt_HD.Text = "";
            txtBox_makh_HD.Text = "";
            txtBox_manv_HD.Text = "";

            dTP_ngayban_HD.CustomFormat = "dd-MM-yyyy";
            dTP_ngayban_HD.Value = DateTime.Now;

            txtBox_tongtien_HD.Text = "";
            txtBox_slban_HD.Text = "";
            txtBox_giaban_HD.Text = "";

            btn_huy_HD.Enabled = false;
            btn_xoa_HD.Enabled = false;
            btn_xemchitiet_HD.Enabled = false;
            txtBox_madonhang_HD.Enabled = false;
        }

        private void getData_fromTable_HD() // lấy dữ liệu từ bảng
        {
            Obj_HD.set_madh(dgv_HD.CurrentRow.Cells["MADH"].Value.ToString());
            Obj_HD.set_mahang(dgv_HD.CurrentRow.Cells["MAHANG"].Value.ToString());
            Obj_HD.set_madt(dgv_HD.CurrentRow.Cells["MADT"].Value.ToString());
            Obj_HD.set_makh(dgv_HD.CurrentRow.Cells["MAKH"].Value.ToString());
            Obj_HD.set_manv(dgv_HD.CurrentRow.Cells["MANV"].Value.ToString());
            Obj_HD.set_ngayban(dgv_HD.CurrentRow.Cells["NGAYBAN"].Value.ToString());
            Obj_HD.set_soluong(dgv_HD.CurrentRow.Cells["SOLUONG"].Value.ToString());
            Obj_HD.set_tongtien(dgv_HD.CurrentRow.Cells["TONGTIEN"].Value.ToString());
        }

        private void LoadData_DonHang() // tải dữ liệu vào DataGridView
        {
            string sql = "SELECT DH.MADH, DH.MAHANG, DH.MADT, DH.MAKH, DH.MANV, DH.NGAYBAN, DT.GIABAN, DH.SOLUONG, DH.TONGTIEN " +
                "FROM DONHANG DH, DIENTHOAI DT " +
                "WHERE DH.MADT = DT.MADT";
            tblHD = Functions.GetDataToTable(sql);
            dgv_HD.DataSource = tblHD;

            // set Font cho tên cột
            dgv_HD.Font = new Font("Time New Roman", 13);
            dgv_HD.Columns[0].HeaderText = "Mã Đơn Hàng";
            dgv_HD.Columns[1].HeaderText = "Mã Hãng";
            dgv_HD.Columns[2].HeaderText = "Mã Điện Thoại";
            dgv_HD.Columns[3].HeaderText = "Mã Khách Hàng";
            dgv_HD.Columns[4].HeaderText = "Mã NHân Viên";
            dgv_HD.Columns[5].HeaderText = "Ngày Bán";
            dgv_HD.Columns[6].HeaderText = "Giá Bán";
            dgv_HD.Columns[7].HeaderText = "Số Lượng Bán";
            dgv_HD.Columns[8].HeaderText = "Tổng Tiền";

            // set Font cho dữ liệu hiển thị trong cột
            dgv_HD.DefaultCellStyle.Font = new Font("Time New Roman", 12);

            // set kích thước cột
            dgv_HD.Columns[0].Width = 150;
            dgv_HD.Columns[1].Width = 110;
            dgv_HD.Columns[2].Width = 160;
            dgv_HD.Columns[3].Width = 170;
            dgv_HD.Columns[4].Width = 160;
            dgv_HD.Columns[5].Width = 120;
            dgv_HD.Columns[6].Width = 120;
            dgv_HD.Columns[7].Width = 150;
            dgv_HD.Columns[8].Width = 150;

            //Không cho người dùng thêm dữ liệu trực tiếp
            dgv_HD.AllowUserToAddRows = false;
            dgv_HD.EditMode = DataGridViewEditMode.EditProgrammatically;

            if (mode_find_HD)
                cbBox_thang_HD.Text = "";
        }

        private void HoaDon_Load(object sender, EventArgs e)
        {
            // set giá trị cho comboBox
            for (int i = 1; i <= 12; i++)
                cbBox_thang_HD.Items.Add(i.ToString());

            setFont_DH();
            ResetValues_HD();
            LoadData_DonHang();
        }
    }
}
