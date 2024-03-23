using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHDT.Class
{
    class hoadon
    {
        private string madh;
        private string makh;
        private string manv;
        private string madt;
        private string soluong;
        private string giamgia;
        private string ngayban;
        private string mahang;
        private string tongtien;

        public hoadon() { }

        public string get_madh() { return madh; }
        public string get_makh() { return makh; }
        public string get_manv() { return manv; }
        public string get_madt() { return madt; }
        public string get_soluong() { return soluong; }
        public string get_ngayban() { return ngayban; }
        public string get_giamgia() { return giamgia; }
        public string get_tongtien() { return tongtien; }
        public string get_mahang() { return mahang; }

        public void set_madh(string value) { madh = value; }
        public void set_makh(string value) { makh = value; }
        public void set_manv(string value) { manv = value; }
        public void set_madt(string value) { madt = value; }
        public void set_soluong(string value) { soluong = value; }
        public void set_ngayban(string value) { ngayban = value; }
        public void set_giamgia(string value) { giamgia = value; }
        public void set_tongtien(string value) { tongtien = value; }
        public void set_mahang(string value) { mahang = value; }

        public bool Check_Data()
        {
            if (manv.Length == 0 | makh.Length == 0 | madt.Length == 0 | soluong.Length == 0
                | giamgia.Length == 0 | tongtien.Length == 0)
                return false;
            return true;
        }

        public void Reset()
        {
            madh = "";
            makh = "";
            manv = "";
            madt = "";
            soluong = "";
            giamgia = "";
            ngayban = "";
            mahang = "";
            tongtien = "";
        }
    }
}
