using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Schema;


namespace Assignment
{
    internal class QuanLySinhVien
    {
        string connectionString = @"Data Source=localhost;Initial Catalog=Asm_C#2;Integrated Security=True;";
        public void NhapSinhVien()
        {
            Console.Write("Nhap so luong sinh vien muon them vao: "); int SoLuong = int.Parse(Console.ReadLine());
            using (AssignmentDataContext db = new AssignmentDataContext(connectionString))
            {
                for (int j = 0; j < SoLuong; j++)
                {
                    Student Student = new Student();
                    do
                    {
                        Console.Write($"Nhap ten sinh vien {j + 1}: "); Student.Name = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(Student.Name))
                        {
                            Console.WriteLine("Ho va ten khong duoc de trong. Vui long nhap lai");
                        }
                    } while (string.IsNullOrWhiteSpace(Student.Name));

                    do 
                    {
                        Console.Write($"Nhap email sinh vien {j + 1}: "); Student.Email = Console.ReadLine();
                        if (!CheckEmail(Student.Email)) {
                            Console.WriteLine("Email nhap vao khong hop le. Vui long nhap lai");
                        }
                    } while (!CheckEmail(Student.Email));

                    do
                    {
                        Console.Write($"Nhap diem cua sinh vien {j + 1}: "); Student.Mark = float.Parse(Console.ReadLine());
                        if (Student.Mark < 0 || Student.Mark > 10)
                        {
                            Console.WriteLine("Diem nhap phai la so thuc tu 0 - 10. Vui long nhap lai");
                        }
                    } while (Student.Mark < 0 || Student.Mark > 10);

                    Console.Write($"Nhap Id lop cua sinh vien {j + 1}: "); Student.IdClass = int.Parse(Console.ReadLine());

                    db.Students.InsertOnSubmit(Student);
                }
                db.SubmitChanges();
            }
        }
        public bool CheckEmail(string email)
        {
            return email.Contains("@") && email.IndexOf('.') > email.IndexOf("@");
        }
        public void XuatDanhSach()
        {
            using (AssignmentDataContext db = new AssignmentDataContext(connectionString))
            {
                db.Students.ToList().ForEach(student => {   Console.Write("Ma sinh vien: "+student.StId+"; Ho va ten: "+student.Name+"; Email: "+student.Email+"; Mark: "+student.Mark+"; Class: "+student.IdClass);
                    if (student.Mark >= 9)
                    {
                        Console.Write("; Hoc luc: Xuat sac");
                    }
                    else if (student.Mark >= 7.5)
                    {
                        Console.Write("; Hoc luc: Gioi");
                    }
                    else if (student.Mark >= 6.5)
                    {
                        Console.Write("; Hoc luc: Kha");
                    }
                    else if (student.Mark >= 5)
                    {
                        Console.Write("; Hoc luc: Trung binh");
                    }
                    else if (student.Mark >= 3)
                    {
                        Console.Write("; Hoc luc: Yeu");
                    }
                    else
                    {
                        Console.Write("; Hoc luc: Yeu");
                    }
                    Console.WriteLine();
                });
            }
        }
        public void SearchInfoBasedOnMarkRange()
        {
            using (AssignmentDataContext db = new AssignmentDataContext(connectionString))
            {
                Console.WriteLine("Nhap khoang Mark");
                    Console.Write("Nhap khoang nho nhat: "); float min = float.Parse(Console.ReadLine());
                    if (min < 0 || min > 10)
                    {
                        Console.WriteLine("Khoang diem nhap vao phai la so thuc tu 0 - 10. Vui long nhap lai");
                        return;
                    }
                    
                Console.Write("Nhap khoang lon nhat: "); float max = float.Parse(Console.ReadLine());
                    if (max < 0 || max > 10)
                    {
                        Console.WriteLine("Khoang diem nhap vao phai la so thuc tu 0 - 10. Vui long nhap lai");
                        return;
                    }
                
                db.Students.Where(s => (s.Mark >= min & s.Mark <= max)).ToList().ForEach(student => {
                    Console.Write("Ma sinh vien: " + student.StId + "; Ho va ten: " + student.Name + "; Email: " + student.Email + "; Mark: " + student.Mark + "; Class: " + student.IdClass);
                    Console.WriteLine();
                });
            }
        }
        public void SearchInfoBasedOnStId()
        {
            string YorN;
            using (AssignmentDataContext db = new AssignmentDataContext(connectionString)) {
                Console.WriteLine("Nhap Id: ");int NhapID= int.Parse(Console.ReadLine());
                db.Students.Where(s => (NhapID == s.StId)).ToList().ForEach(student => {
                    Console.WriteLine("Ma sinh vien: " + student.StId + "; Ho va ten: " + student.Name + "; Email: " + student.Email + "; Mark: " + student.Mark + "; Class: " + student.IdClass);
                    do
                    {
                        Console.Write("Co muon cap nhat thong tin sinh vien nay hay khong ?(Chon y/n): "); YorN = Console.ReadLine();
                        if (YorN.Equals("y", StringComparison.OrdinalIgnoreCase))
                        {
                            do
                            {
                                Console.Write($"Nhap ten sinh vien: "); student.Name = Console.ReadLine();
                                if (string.IsNullOrWhiteSpace(student.Name))
                                {
                                    Console.WriteLine("Ho va ten khong duoc de trong. Vui long nhap lai");
                                }
                            } while (string.IsNullOrWhiteSpace(student.Name));

                            do
                            {
                                Console.Write($"Nhap email sinh vien: "); student.Email = Console.ReadLine();
                                if (!CheckEmail(student.Email))
                                {
                                    Console.WriteLine("Email nhap vao khong hop le. Vui long nhap lai");
                                }
                            } while (!CheckEmail(student.Email));

                            do
                            {
                                Console.Write($"Nhap diem cua sinh vien: "); student.Mark = float.Parse(Console.ReadLine());
                                if (student.Mark < 0 && student.Mark > 10)
                                {

                                }
                            } while (student.Mark < 0 && student.Mark > 10);
                            Console.Write($"Nhap Id lop cua sinh vien: "); student.IdClass = int.Parse(Console.ReadLine());
                            db.SubmitChanges();
                        }
                        else if (YorN.Equals("n", StringComparison.OrdinalIgnoreCase))
                        {
                            return;
                        }
                        else
                        {
                            Console.WriteLine("chi duoc chon y/n");
                        }
                    }while( YorN !=  "y" && YorN != "n");
                });
            }
        }
        public void RenderStudentBasedOnDescMark()
        {
            using (AssignmentDataContext db = new AssignmentDataContext(connectionString))
            {
                db.Students.OrderByDescending(s => (s.Mark)).ToList().ForEach(student => {
                    Console.Write("Ma sinh vien: " + student.StId + "; Ho va ten: " + student.Name + "; Email: " + student.Mark + "; Mark: " + student.Mark + "; Class: " + student.IdClass);
                    Console.WriteLine();
                });
            }
        }
        public void RenderTop5BestMarkStudent()
        {
            using (AssignmentDataContext db = new AssignmentDataContext(connectionString))
            {
                db.Students.OrderByDescending( s => (s.Mark)).Take(5).ToList().ForEach(student => {
                    Console.Write("Ma sinh vien: " + student.StId + "; Ho va ten: " + student.Name + "; Email: " + student.Mark + "; Mark: " + student.Mark + "; Class: " + student.IdClass);
                    Console.WriteLine();
                });
            }
        }
    }
}
