using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Threading;

namespace Assignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int chucnang = 0;
            QuanLySinhVien quanLySinhVien = new QuanLySinhVien();
            QuanLyLop quanLyLop = new QuanLyLop();
            do
            {
                Console.WriteLine();
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("              Chuong trinh quan ly hoc vien                    ");
                Console.WriteLine("1.Nhap thong tin Student");
                Console.WriteLine("2.Nhap thong tin Class");
                Console.WriteLine("3.Xuat danh sach hoc vien");
                Console.WriteLine("4.Tim kiem hoc vien theo khoang Mark");
                Console.WriteLine("5.Tim hoc vien theo StId va cap nhat thong tin hoc vien");
                Console.WriteLine("6.Xuat hoc vien ra man hinh theo thu tu diem tu cao toi thap");
                Console.WriteLine("7.Xuat 5 hoc vien co diem cao nhat");
                Console.WriteLine("8.Tinh diem trung binh tung lop");
                Console.WriteLine("9.Thoat");
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("Chon tu 1-9");
                chucnang = int.Parse(Console.ReadLine());
                Console.WriteLine();
                switch (chucnang)
                {
                    case 1:
                        quanLySinhVien.NhapSinhVien();
                        break;
                    case 2:
                        quanLyLop.NhapLop();
                        break;
                    case 3:
                        quanLySinhVien.XuatDanhSach();
                        break;
                    case 4:
                        quanLySinhVien.SearchInfoBasedOnMarkRange();
                        break;
                    case 5:
                        quanLySinhVien.SearchInfoBasedOnStId();
                        break;
                    case 6:
                        quanLySinhVien.RenderStudentBasedOnDescMark();
                        break;
                    case 7:
                        quanLySinhVien.RenderTop5BestMarkStudent();
                        break;
                    case 8:
                        Thread DTB = new Thread(new ThreadStart(quanLyLop.CalculateAvgForEachClass));
                        DTB.Start();
                        break;
                    case 9:
                        Console.WriteLine("Thoat chuong trinh");
                        break;
                    default:
                        Console.WriteLine("Chuc nang chon khong hop le (chon tu 1-9)");
                        break;
                }

            } while (chucnang != 9);
        }
    }
}
