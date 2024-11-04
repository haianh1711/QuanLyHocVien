using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Xml.Linq;
using System.Security.Cryptography;

namespace Assignment
{
    internal class QuanLyLop
    {
        string connectionString = @"Data Source=localhost;Initial Catalog=Asm_C#2;Integrated Security=True;";
        public void NhapLop()
        {
            Console.Write("Nhap so luong lop muon them vao: "); int SoLuong = int.Parse(Console.ReadLine());
            using (AssignmentDataContext db = new AssignmentDataContext(connectionString)) {
                for (int i = 0; i < SoLuong; i++)
                {
                    Class Lop = new Class();
                    Console.Write($"Nhap ten cho Class {i + 1}: "); Lop.NameClass = Console.ReadLine();
                    db.Classes.InsertOnSubmit(Lop);
                }
                db.SubmitChanges();
            }
        }
        public void CalculateAvgForEachClass()
        {
            string filePath = "C:\\Users\\admin\\Documents\\BAITAP\\C#\\Asm_C#2.txt";
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                using (AssignmentDataContext db = new AssignmentDataContext(connectionString))
                {
                    db.Classes.OrderBy(c => (c.IdClass)).ToList().ForEach(c =>
                    {
                        int count = 0;
                        double SumMark = 0;
                        db.Students.ToList().ForEach(student =>
                        {
                            if (student.IdClass == c.IdClass)
                            {
                                count++;
                                SumMark += student.Mark;
                            }
                        });
                        double AvgMark = SumMark / count;
                        Console.WriteLine($"Diem trung binh cua lop {c.NameClass} la: {AvgMark}");
                        writer.Write($"Diem trung binh cua lop {c.NameClass} la: {AvgMark}");
                    });
                }
            }
        }
    }
}
