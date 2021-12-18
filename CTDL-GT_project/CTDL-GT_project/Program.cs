using System;
using System.Collections.Generic;
using System.IO;

namespace CTDL_GT_project
{
    public class Phonebook
    {
        private string id;
        private string nickname;
        private string number;
        private string email;
        private string facebook;
        private string birthday;

        public Phonebook(string id, string nickname, string number, string email, string facebook, string birthday)
        {
            this.id = id;
            this.nickname = nickname;
            this.number = number;
            this.email = email;
            this.facebook = facebook;
            this.birthday = birthday;
        }
        public string getId()
        {
            return id;
        }
        public string getNickname()
        {
            return nickname;
        }
        public string getNumber()
        {
            return number;
        }
        public string getEmail()
        {
            return email;
        }
        public string getFacebook()
        {
            return facebook;
        }
        public string getBirthday()
        {
            return birthday;
        }
        override public string ToString()
        {
            return "Phonebook(" + id + ", " + nickname + ", " + number + ", " + email + ", " + facebook + ", " + birthday + ")";
        }
    }
    public class Node
    {
        public Phonebook element;
        public Node flink, blink;
        public Node()
        {
            element = null;
            flink = blink = null;
        }
        public Node(Phonebook element)
        {
            this.element = element;
            flink = blink = null;
        }
    }
    public class DoubleLinkedList
    {
        public Node header;
        public DoubleLinkedList()
        {
            header = new Node(new Phonebook("00", "null", "0000000000", "xxx@gmail.com", "www.facebook.com/null", "00/00/0000"));
        }

        // CHỨC NĂNG CHÈN
        private Node Find(string input)
        {
            Node current = header;
            int count = 0;
            while ((current != null) && (current.element.getId() != input)
                && (current.element.getNickname() != input)
                && (current.element.getNumber() != input)
                && (current.element.getEmail() != input)
                && (current.element.getFacebook() != input)
                && (current.element.getBirthday() != input))
            {
                count++;
                current = current.flink;
            }
            if (count == 0)
                return null;
            return current;
        }
        public void Add(Phonebook newPhonebook)
        {
            Node current = header;
            Node newnode = new Node(newPhonebook);
            newnode.flink = current.flink;
            newnode.blink = current;
            current.flink = newnode;
            if (newnode.flink != null)
            {
                newnode.flink.blink = newnode;
            }
        }
        private Phonebook Input()
        {
            Phonebook db;
            Console.Write("> ID: ");
            string idne = (Console.ReadLine());
            Console.Write("> Nickname: ");
            string nickname = Console.ReadLine();
            Console.Write("> Number: ");
            string phonenumber = Console.ReadLine();
            Console.Write("> Email: ");
            string email = Console.ReadLine();
            Console.Write("> Facebook URL: ");
            string fb = Console.ReadLine();
            Console.Write("> Birthday: ");
            string bt = Console.ReadLine();
            db = (new Phonebook(idne, nickname, phonenumber, email, fb, bt));
            return db;
        }
        public void AddContact()
        {
            Phonebook input = Input();
            Add(input);
        }

        // CHỨC NĂNG XÓA
        public void Remove(List<Phonebook> recyclebin, string input)
        {
            Node current = Find(input);
            if (current.flink != null)
            {
                current.blink.flink = current.flink;
                current.flink.blink = current.blink;
                current.flink = null;
                current.blink = null;
            }
            else
            {
                current.blink.flink = null;
                current.blink = null;
            }
            recyclebin.Add(current.element);
        }

        // CHỨC NĂNG SẮP XẾP 
        private void Swap(ref Phonebook a, ref Phonebook b)
        {
            Phonebook temp = a;
            a = b;
            b = temp;
        }
        private void SortId(DoubleLinkedList dlist)
        {
            Console.WriteLine("\nDanh bạ đã sắp xếp theo Id: ");
            for (Node k = dlist.header.flink; k.flink != null; k = k.flink)
            {
                for (Node h = k.flink; h != null; h = h.flink)
                {
                    if (String.Compare(k.element.getId(), h.element.getId()) < 0)
                    {
                        Swap(ref k.element, ref h.element);
                    }
                }
            }
            dlist.Print();
        }
        private void SortName(DoubleLinkedList dlist)
        {
            Console.WriteLine("\nDanh bạ đã sắp xếp theo Nickname: ");
            for (Node k = dlist.header.flink; k.flink != null; k = k.flink)
            {
                for (Node h = k.flink; h != null; h = h.flink)
                {
                    if (String.Compare(k.element.getNickname(), h.element.getNickname()) < 0)
                    {
                        Swap(ref k.element, ref h.element);
                    }
                }
            }
            dlist.Print();
        }
        public void Sort(DoubleLinkedList pbook)
        {
        trinhne:
            Console.WriteLine("[1] Sắp xếp theo Id");
            Console.WriteLine("[2] Sắp xếp theo Tên");
            Console.Write("\nNhập lựa chọn của bạn: ");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    Console.Clear();
                    SortId(pbook);
                    break;
                case "2":
                    Console.Clear();
                    SortName(pbook);
                    break;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng nhập lại!");
                    Console.ReadKey();
                    Console.Clear();
                    goto trinhne;
            }
        }

        // Thêm danh bạ        
        public void GetData(List<string> newlist)
        {
            Node current = FindLast();
            while (current.blink != null)
            {
                newlist.Add(current.element.getId() + "-"
                    + current.element.getNickname() + "-"
                    + current.element.getNumber() + "-"
                    + current.element.getEmail() + "-"
                    + current.element.getFacebook() + "-"
                    + current.element.getBirthday());
                current = current.blink;
            }
        }

        // Cập nhật danh bạ
        public void UpdatePhone()
        {
            Console.Clear();
            Print();
            Console.Write("\nNhập ID/Nickname/SDDT/EMail/Facebook của liên lạc cần sửa: ");
            string input = Console.ReadLine();
            Node current = Find(input);
            if (current == null)
            {
                Console.WriteLine("\nLiên lạc không tồn tại hoặc nhập sai. Nhấn [Enter] để nhập lại");
                Console.ReadKey();
                UpdatePhone();
            }
            Phonebook newdb;
            Console.WriteLine("\nNhập lại thông tin: ");
            newdb = Input();
            current.element = newdb;
        }

        // chức năng tìm kiếm
        public void FindPlus()
        {
        FindPlus:
            Console.Clear();
            Console.Write("\n \n(-) Tìm kiếm nhanh\t(Phím 1)\n(-) Tìm kiếm Id \t(Phím 2)\n(-) Tìm kiếm Tên\t(Phím 3)\n(-) Tìm kiếm Số ĐT\t(Phím 4)\n(-) Tìm kiếm Email\t(Phím 5)\n(-) Tìm kiếm FaceBook\t(Phím 6)\n(-) Tìm kiếm Sinh Nhật\t(Phím 7)\n(-) Thoát\t(Phím 0)\n\nLựa chọn tìm kiếm: ");
            string opts = Console.ReadLine();

            switch (opts)
            {
                case "0": return;
                case "1":
                    {
                        Console.Clear();
                        int count = 0;
                        Console.Write("\nNhập từ khóa cần tìm: ");
                        string key = Console.ReadLine();
                        key = key.ToLower();
                        Node current = FindLast();
                        while (current.blink != null)
                        {
                            if (current.element.getBirthday().Contains(key) || current.element.getFacebook().ToLower().Contains(key) || current.element.getEmail().ToLower().Contains(key) || current.element.getNumber().Contains(key) || current.element.getNickname().ToLower().Contains(key))
                            {
                                Console.WriteLine(current.element);
                                count++;
                            }
                            current = current.blink;
                        }
                        if (count == 0) Console.WriteLine("Không có kết quả phù hợp!!");
                        Console.WriteLine("Nhấn [Enter] để tiếp tục");
                        Console.ReadKey();
                    }; goto FindPlus;
                case "2":
                    {
                        Console.Clear();
                        int count = 0;
                        Console.Write("\nNhập Id cần tìm: ");
                        string key = Console.ReadLine();
                        key = key.ToLower();
                        Node current = FindLast();
                        while (current.blink != null)
                        {
                            if (current.element.getId().ToLower().Contains(key))
                            {
                                Console.WriteLine(current.element.getId() + ":  " + current.element.getNickname() + "(" + current.element.getNumber() + ")");
                                count++;
                            }
                            current = current.blink;
                        }

                        if (count == 0) Console.WriteLine("Không có kết quả phù hợp!!");
                        Console.ReadKey();
                    }; goto FindPlus;
                case "3":
                    {
                        Console.Clear();
                        int count = 0;
                        Console.Write("\nNhập tên cần tìm: ");
                        string key = Console.ReadLine();
                        key = key.ToLower();
                        Node current = FindLast();
                        while (current.blink != null)
                        {
                            if (current.element.getNickname().ToLower().Contains(key))
                            {
                                Console.WriteLine(current.element.getNickname() + ":  " + current.element.getNumber());
                                count++;
                            }
                            current = current.blink;
                        }
                        Console.ReadKey();
                        if (count == 0) Console.WriteLine("Không có kết quả phù hợp!!");
                        Console.ReadKey();
                    }; goto FindPlus;
                case "4":
                    {
                        Console.Clear();
                        int count = 0;
                        Console.Write("\nNhập số điện thoạt cần tìm: ");
                        string key = Console.ReadLine();
                        Node current = FindLast();
                        while (current.blink != null)
                        {
                            if (current.element.getNumber().Contains(key))
                            {
                                Console.WriteLine(current.element.getNumber() + ":  " + current.element.getNickname());
                                count++;
                            }
                            current = current.blink;
                        }
                        Console.ReadKey();
                        if (count == 0) Console.WriteLine("Không có kết quả phù hợp!!");
                        Console.ReadKey();
                    }; goto FindPlus;
                case "5":
                    {
                        Console.Clear();
                        int count = 0;
                        Console.Write("\nNhập email cần tìm: ");
                        string key = Console.ReadLine();
                        key = key.ToLower();
                        Node current = FindLast();
                        while (current.blink != null)
                        {
                            if (current.element.getEmail().ToLower().Contains(key))
                            {
                                Console.WriteLine(current.element.getEmail() + ":  " + current.element.getNickname() + "(" + current.element.getNumber() + ")");
                                count++;
                            }
                            current = current.blink;
                        }
                        Console.ReadKey();
                        if (count == 0) Console.WriteLine("Không có kết quả phù hợp!!");
                        Console.ReadKey();
                    }; goto FindPlus;
                case "6":
                    {
                        Console.Clear();
                        int count = 0;
                        Console.Write("\nNhập facebook cần tìm: ");
                        string key = Console.ReadLine();
                        key = key.ToLower();
                        Node current = FindLast();
                        while (current.blink != null)
                        {
                            if (current.element.getFacebook().ToLower().Contains(key))
                            {
                                Console.WriteLine(current.element.getFacebook() + ":  " + current.element.getNickname() + "(" + current.element.getNumber() + ")");
                                count++;
                            }
                            current = current.blink;
                        }
                        Console.ReadKey();
                        if (count == 0) Console.WriteLine("Không có kết quả phù hợp!!");
                        Console.ReadKey();
                    }; goto FindPlus;
                case "7":
                    {
                        Console.Clear();
                        int count = 0;
                        Console.Write("\nNhập ngày sinh nhật cần tìm: ");
                        string key = Console.ReadLine();
                        Node current = FindLast();
                        while (current.blink != null)
                        {
                            if (current.element.getBirthday().Contains(key))
                            {
                                Console.WriteLine(current.element.getBirthday() + ":  " + current.element.getNickname() + "(" + current.element.getNumber() + ")");
                                count++;
                            }
                            current = current.blink;
                        }
                        Console.ReadKey();
                        if (count == 0) Console.WriteLine("Không có kết quả phù hợp!!");
                        Console.ReadKey();
                    }; goto FindPlus;
                default:
                    Console.WriteLine("Ký tự bạn nhập không phù hợp. Xin hãy nhập lại"); Console.ReadKey(); goto FindPlus;
            }
        }

        // Thùng rác
        public void PrintBin(List<Phonebook> recyclebin, DoubleLinkedList dlist, string recyclePath)
        {
            Console.Clear();
            if (recyclebin.Count == 0)
            {
                Console.WriteLine("\nThùng rác rỗng");
                return;
            }

            Console.WriteLine("Thùng rác: ");
            foreach (Phonebook st in recyclebin)
            {
                Console.WriteLine(st);
            }
            Console.WriteLine("\n[1] Khôi phục ");
            Console.WriteLine("[2] Xóa vĩnh viễn ");
            Console.Write("\nChọn yêu cầu của bạn: ");
            string luachon = Console.ReadLine();
            switch (luachon)
            {
                case "1":
                    foreach (var item in recyclebin)
                    {
                        dlist.Add(item);
                    }
                    Console.WriteLine("\nKhôi phục thành công");
                    recyclebin.Clear();

                    break;
                case "2":
                    recyclebin.Clear();
                    Console.WriteLine("\nCác liên lạc trong thùng rác đã bị xóa");
                    break;
                default:
                    {
                        Console.WriteLine("\nYêu cầu không hợp lệ. Nhấn [Enter] để thực hiện lại chức năng!");
                        Console.ReadKey();
                        PrintBin(recyclebin, dlist, recyclePath);
                        break;
                    }
            }
        }

        // Gọi điện
        public void CallLogs(List<Phonebook> callLogs)
        {
            Console.Clear();
            Console.SetCursorPosition((Console.WindowWidth - "NHẬT KÝ CUỘC GỌI".Length) / 2, Console.CursorTop);
            Console.WriteLine("NHẬT KÝ CUỘC GỌI\n");
            Console.WriteLine("(1)- In nhật ký cuộc gọi: ");
            Console.WriteLine("(2)- Xóa hết nhật ký cuộc gọi: ");
            Console.WriteLine("(3)- Xóa 1 nhật ký cuộc gọi: ");
            Console.WriteLine("(0)- Thoát");
            Console.Write("\nNhập lựa chọn của bạn: ");
            int count = 0;
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "0": return;
                case "1":
                    Console.Clear();
                    if (callLogs.Count == 0)
                        Console.WriteLine("\nNhật ký rỗng ");
                    for (int i = 0; i < callLogs.Count; i++)
                    {
                        Console.WriteLine("Đã gọi cho " + callLogs[i].getNumber() + " vào " + callLogs[i].getFacebook());
                    }
                    Console.WriteLine("\n[Enter] để tiếp tục");
                    Console.ReadKey();
                    break;
                case "2":
                    callLogs.Clear();
                    Console.WriteLine("\nĐã xóa hết nhật ký cuộc gọi");
                    Console.WriteLine("[Enter] để tiếp tục");
                    Console.ReadKey();
                    break;
                case "3":
                    Console.Clear();
                    for (int i = 0; i < callLogs.Count; i++)
                    {
                        Console.WriteLine("Đã gọi cho " + callLogs[i].getNumber() + " vào " + callLogs[i].getFacebook());
                    }
                    Console.Write("Nhập số bạn muốn xóa nhật ký cuộc gọi: ");
                    string number = Console.ReadLine();
                    for (int i = 0; i < callLogs.Count; i++)
                    {
                        if (String.Compare(number, callLogs[i].getNumber()) == 0)
                        {
                            callLogs.Remove(callLogs[i]);
                            count++;
                            Console.WriteLine("Xóa thành công. Nhấn [Enter] để tiếp tục");
                        }
                    }
                    if (count == 0) Console.WriteLine("Không tìm thấy liên lạc. Nhấn [Enter] để tiếp tục");
                    Console.ReadKey();
                    break;
            }
            CallLogs(callLogs);
        }

        // Nhật ký
        public void Call(DoubleLinkedList pbook, List<Phonebook> callLogs)
        {
            Console.WriteLine("[Phím 1] - Nhập số để gọi.");
            Console.WriteLine("[Phím 2] - Gọi số có sẵn trong danh bạ.");
            double t1, t2;
            string time;
            string t = DateTime.Now.ToString();
            Console.Write("\nLựa chọn của bạn: ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Clear();
                    Console.Write("Nhập số điện thoại bạn muốn gọi: ");
                    string number = Console.ReadLine();
                    t1 = DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds;
                    Console.Clear();
                    Console.WriteLine("\nĐang gọi cho " + number + " ...");
                    Console.Write("\nBấm [Enter] để kết thúc cuộc gọi: ");
                    Console.ReadLine();
                    t2 = DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds;
                    time = Convert.ToString(t2 - t1);
                    Console.WriteLine("\nThời gian thực hiện cuộc gọi: " + time + " giây");
                    Console.WriteLine("Thời điểm: {0}", t);
                    callLogs.Add(new Phonebook("00", "0", number, time, t, "0"));
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("Danh bạ hiện có: ");
                    pbook.Print();
                    Console.Write("Nhập tên muốn gọi: ");
                    string input = Console.ReadLine();
                    Node node = pbook.Find(input);
                    t1 = DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds;
                    Console.Clear();
                    Console.WriteLine("\nĐang gọi cho " + node.element.getNickname() + " ....");
                    Console.Write("\nBấm [Enter] để kết thúc cuộc gọi. ");
                    Console.ReadLine();
                    t2 = DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds;
                    time = Convert.ToString(t2 - t1);
                    Console.WriteLine("\nThời gian thực hiện cuộc gọi: " + (t2 - t1) + " giây");
                    Console.WriteLine("Thời điểm: {0}", t);
                    callLogs.Add(new Phonebook("0", "0", node.element.getNumber(), time, t, "0"));
                    break;
            }
        }

        // Gửi tin nhắn
        public void SendSMS()
        {
            Console.WriteLine("\n[1] Gửi tin nhắn cho người mới.");
            Console.WriteLine("[2] Gửi tin nhắn cho người đã có trong danh bạ.");
            Console.Write("\nNhập lựa chọn của bạn: ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                chinhchinh:
                    Console.Clear();
                    Console.Write("\nNhập số điện thoại người nhận: ");
                    string number = Console.ReadLine();
                    int Result;
                    bool isNumber = int.TryParse(number, out Result);
                    if (isNumber)
                    {
                        if (isNumber && number.Length == 10)
                        {
                            Console.Write("Nhập nội dung: ");
                            string content = Console.ReadLine();
                            Console.WriteLine($"Đã gửi tin nhắn đến số điện thoại {number} với nội dung: {content}. \nVào lúc {DateTime.Now}");
                        }
                        else
                        {
                            Console.WriteLine("Số điện thoại không hợp lệ! Vui lòng nhập lại!");
                            Console.ReadKey();
                            goto chinhchinh;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Số điện thoại không hợp lệ! Vui lòng nhập lại!");
                        Console.ReadKey();
                        goto chinhchinh;
                    }
                    break;
                case "2":
                begin:
                    Console.Clear();
                    Print();
                    Console.Write("\nNhập SĐT người nhận: ");
                    string nickname = Console.ReadLine();
                    Node node = Find(nickname);
                    if (node != null)
                    {
                        Console.Write("Nhập nội dung: ");
                        string content1 = Console.ReadLine();
                        Console.WriteLine($"Đã gửi tin nhắn đến số điện thoại {nickname} với nội dung: {content1}. \nVào lúc {DateTime.Now}");
                    }
                    else
                    {
                        Console.WriteLine("Tên không có trong danh bạ.");
                        goto begin;
                    }
                    break;
            }
        }

        //Sao lưu và khôi phục
        public void BaR()
        {
        BaR:
            Console.Clear();
            string FileBackup = "Backup.txt";
            string Fileinput = "DanhBa.txt";
            Console.Write("(-) Sao lưu\t\t(Phím 1)\n(-) Khôi Phục\t\t(Phím 2)\n(-) Xóa danh bạ \t(Phím 3)\n(-) Trở về Menu\t\t(Phím 0)\n\nLựa chọn: ");
            string opts = Console.ReadLine();
            switch (opts)
            {
                case "0": return;
                case "1":
                    {
                        List<string> newlist = new List<string>();
                        GetData(newlist);
                        File.WriteAllText(FileBackup, string.Empty);
                        StreamWriter sw = File.AppendText(FileBackup);
                        for (int i = 0; i < newlist.Count; i++)
                            sw.WriteLine(newlist[i]);
                        sw.Close();
                        Console.WriteLine("\nQuá trình sao lưu hoàn tất!!");
                    }; break;
                case "2":
                    {
                        if (File.Exists("Backup.txt"))
                        {
                            File.Delete(Fileinput);
                            File.Copy(FileBackup, Fileinput);
                            Console.WriteLine("\nQuá trình khôi phục hoàn tất!!");
                        }
                        else
                            Console.WriteLine("\nBạn chưa có bản sao lưu");

                    }; break;
                case "3":
                    {
                        Console.WriteLine("Bạn có chắc chắn muốn khôi phục nguyên bản?\n\t Có (Phím 1)\t Không (Phím bất kì)");
                        string co = Console.ReadLine();
                        if (co == "1")
                        {
                            File.Delete(Fileinput);
                            using (File.Create(Fileinput)) { }
                            Console.WriteLine("Quá trình khôi phục nguyên bản hoàn tất!!");
                        }
                    }; break;
                default:
                    Console.WriteLine("Lựa chọn bạn nhập không đúng. Xin nhập lại"); goto BaR;
            }
        }

        // Danh bạ riêng tư
        public void PrivateSector(DoubleLinkedList pbook, DoubleLinkedList privateBook)
        {
            Console.Clear();
            if (!File.Exists("password.txt"))
            {
                StreamWriter sw = File.AppendText("password.txt");
                sw.WriteLine("123abc");
                sw.Close();
            }

            StreamReader sr = File.OpenText("password.txt");
            string currentPassword = sr.ReadLine();
            sr.Close();

            int attempt = 3;
            while (attempt != 0)
            {
            inputPW:
                if (attempt == 0) return;
                attempt--;
                Console.Write("Nhập mật khẩu: "); string password = Console.ReadLine();
                if (password != currentPassword)
                {
                    Console.WriteLine("Mật khẩu sai. Bạn còn {0} lần nhập", attempt);
                    goto inputPW;
                }
                else goto beginloop;
            }
        beginloop:
            Console.Clear();
            Console.SetCursorPosition((Console.WindowWidth - "PRIVATE PHONEBOOK".Length) / 2, Console.CursorTop);
            Console.WriteLine("PRIVATE PHONEBOOK\n");
            Console.WriteLine("(0) - In danh sách hiện có.");
            Console.WriteLine("(1) - Thay đổi mật khẩu.");
            Console.WriteLine("(2) - Thêm số vào danh sách.");
            Console.WriteLine("(3) - Xóa số khỏi danh sách.");
            Console.WriteLine("(4) - Thoát khỏi chương trình.");
            Console.Write("Nhập lựa chọn của bạn: ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "0":
                    Console.Clear();
                    privateBook.Print();
                    Console.ReadKey();
                    goto beginloop;
                case "1":
                    Console.Clear();
                    int i = 0;
                    while (i < 3)
                    {
                        Console.Write("\nNhập mật khẩu cũ: ");
                        string oldPassword = Console.ReadLine();
                        if (oldPassword == currentPassword)
                        {
                            Console.Write("\nNhập mật khẩu mới: ");
                            string newPassword = Console.ReadLine();
                            if (newPassword == currentPassword)
                            {
                                Console.WriteLine("Mật khẩu mới không thể giống mật khẩu cũ");
                            }
                            else
                            {
                                File.WriteAllText("password.txt", string.Empty);
                                StreamWriter sw = File.AppendText("password.txt");
                                sw.WriteLine(newPassword);
                                sw.Close();
                                Console.WriteLine("Mật khẩu thay đổi thành công.");
                                Console.ReadKey();
                            }
                            i = 3;
                        }
                        else
                        {
                            if (i == 2)
                            {
                                Console.WriteLine("Bạn tạm thời không được đổi mật khẩu.");
                            }
                            else
                            {
                                Console.Write("\nMật khẩu không chính xác. Vui lòng nhập lại");
                                Console.WriteLine("Bạn còn {0} lần nhập.", (2 - i));
                            }
                            i++;
                        }
                    }
                    goto beginloop;
                case "2":
                loop1:
                    Console.Clear();
                    pbook.Print();
                    Console.Write("\nNhập liên lạc muốn thêm vào danh sách riêng tư: ");
                    string name = Console.ReadLine();
                    Node node = pbook.Find(name);
                    if (node == null)
                    {
                        Console.WriteLine("Không tìm thấy kết quả");
                    }
                    else
                    {
                        privateBook.Add(new Phonebook(node.element.getId(), node.element.getNickname(), node.element.getNumber(), node.element.getEmail(), node.element.getFacebook(), node.element.getBirthday()));
                        Console.WriteLine("Đã thêm {0} vào danh bạ bí mật ", node.element);
                    }
                    goto beginloop;
                case "3":
                    Console.Clear();
                    privateBook.Print();
                    Console.WriteLine("\nNhập số cần xóa: ");
                    string name1 = Console.ReadLine();
                    Node current = privateBook.Find(name1);
                    if (current == null)
                    {
                        Console.WriteLine("Danh bạ rỗng, vui lòng thêm vào!");
                        goto loop1;
                    }
                    else
                    {
                        if (current.flink != null)
                        {
                            current.blink.flink = current.flink;
                            current.flink.blink = current.blink;
                            current.flink = null;
                            current.blink = null;
                        }
                        else
                        {
                            current.blink.flink = null;
                            current.blink = null;
                        }
                        Console.WriteLine("Đã xóa {0} khỏi danh bạ bí mật.", current.element);
                    }
                    goto beginloop;
                case "4":
                    Console.WriteLine("\nĐã thoát Danh bạ riêng tư");
                    goto endloop;
                default:
                    Console.WriteLine("Lựa chọn của bạn không có trong menu\nVui lòng chọn lại");
                    break;
            }
            goto beginloop;
        endloop:
            Console.WriteLine();
        }

        // Sinh nhật
        public void HPBD(DoubleLinkedList pbook)
        {
            Node current = header;
            int count = 0;
            while (current != null)
            {
                string date = DateTime.Now.ToString("dd/MM/yyyy");
                if (date.Contains(current.element.getBirthday().Substring(0, 5)))
                {
                    count++;
                    Console.WriteLine("Hôm nay là sinh nhật của {0}", current.element.getNickname());
                    Console.WriteLine("Nhấn phím 1 nếu bạn muốn gửi lời chúc đến {0}", current.element.getNickname());
                    Console.WriteLine("Nhấn phím 2 nếu bạn đang bận");
                    Console.Write("Lựa chọn của bạn: ");
                    string sinhnhat = Console.ReadLine();
                    switch (sinhnhat)
                    {
                        case "1":
                            Console.Write("Nhập lời chúc của bạn: ");
                            string content1 = Console.ReadLine();
                            Console.WriteLine($"Bạn đã gửi lời chúc đến {current.element.getNickname()} với nội dung: {content1}.");
                            break;
                        case "2":
                            break;
                    }
                }
                current = current.flink;
            }
            if (count == 0)
            {
                Console.WriteLine("Hôm nay không có sinh nhật của ai!");
            }
        }

        // tìm để in
        private Node FindLast()
        {
            Node current = header;
            while (!(current.flink == null))
                current = current.flink;
            return current;
        }
        // in
        public void Print()
        {
            Node current = FindLast();
            if (current.blink != null)
            {
                while (!(current.blink == null))
                {
                    Console.WriteLine(current.element);
                    current = current.blink;
                }
            }
            else
            {
                Console.WriteLine("Danh sách rỗng");
            }
        }

        // Các hàm đọc từ .txt vào biến trong chương trình
        public List<Phonebook> GetCallLogs(string logPath)
        {
            List<Phonebook> callLogs = new List<Phonebook>();
            List<string> list = new List<string>();
            ReadFile(logPath, list);
            if (new FileInfo(logPath).Length != 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    var result = list[i].Split('-');
                    callLogs.Add(new Phonebook(result[0], result[1], (result[2]), result[3], result[4], result[5]));
                }
            }
            return callLogs;
        }
        public DoubleLinkedList GetPrivateSector(string privatePath)
        {
            DoubleLinkedList privatebook = new DoubleLinkedList();
            List<string> list = new List<string>();
            ReadFile(privatePath, list);
            if (new FileInfo(privatePath).Length != 0)
            {
                AddToPhonebook(list, privatebook);
            }
            return privatebook;
        }
        public List<Phonebook> GetRecycleBin(string recyclePath)
        {
            List<Phonebook> recycleBin = new List<Phonebook>();
            List<string> list = new List<string>();
            ReadFile(recyclePath, list);
            if (new FileInfo(recyclePath).Length != 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    var result = list[i].Split('-');
                    recycleBin.Add(new Phonebook(result[0], result[1], (result[2]), result[3], result[4], result[5]));
                }
            }
            return recycleBin;
        }
        public List<string> ReadFile(string filepath, List<string> result)
        {
            if (File.Exists(filepath))
            {
                StreamReader sr = File.OpenText(filepath);
                String s = "";
                do
                {
                    s = sr.ReadLine();
                    result.Add(s);
                } while (!sr.EndOfStream);
                sr.Close();
            }
            else
            {
                using (File.Create(filepath)) { }
                Console.WriteLine("File Danh bạ tạo thành công.");
            }
            return result;
        }
        public void AddToPhonebook(List<string> list, DoubleLinkedList pbook)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var result2 = list[i].Split('-');
                pbook.Add(new Phonebook(result2[0], result2[1], (result2[2]), result2[3], result2[4], result2[5]));
            }
        }

        // Các hàm lưu từ biến vào .txt
        private void Write2DanhBa(string path)
        {
            List<string> newlist = new List<string>();
            GetData(newlist);
            File.WriteAllText(path, string.Empty);
            StreamWriter sw = File.AppendText(path);
            for (int i = 0; i < newlist.Count; i++)
                sw.WriteLine(newlist[i]);
            sw.Close();
        }

        public void Write2Bin(string recyclePath, List<Phonebook> recycleBin)
        {
            File.WriteAllText(recyclePath, string.Empty);
            StreamWriter sw = File.AppendText(recyclePath);
            foreach (var line in recycleBin)
            {
                sw.WriteLine("{0}-{1}-{2}-{3}-{4}-{5}", line.getId(), line.getNickname(), line.getNumber(), line.getEmail(), line.getFacebook(), line.getBirthday());
            }
            sw.Close();
        }

        public void Write2Log(string logPath, List<Phonebook> callLogs)
        {
            File.WriteAllText(logPath, string.Empty);
            StreamWriter sw = File.AppendText(logPath);
            foreach (var line in callLogs)
            {
                sw.WriteLine("{0}-{1}-{2}-{3}-{4}-{5}", line.getId(), line.getNickname(), line.getNumber(), line.getEmail(), line.getFacebook(), line.getBirthday());
            }
            sw.Close();
        }

        public void Write2Private(string privatePath, DoubleLinkedList privateBook)
        {
            List<string> newlist = new List<string>();
            privateBook.GetData(newlist);
            File.WriteAllText(privatePath, string.Empty);
            StreamWriter sw = File.AppendText(privatePath);
            for (int i = 0; i < newlist.Count; i++)
                sw.WriteLine(newlist[i]);
            sw.Close();
        }

        public void Quicksave(string filepath, string recyclePath, string logPath, string privatePath, DoubleLinkedList privateBook, List<Phonebook> recycleBin, List<Phonebook> callLogs)
        {
            Write2DanhBa(filepath);
            Write2Bin(recyclePath, recycleBin);
            Write2Log(logPath, callLogs);
            Write2Private(privatePath, privateBook);
        }
    }

    class Program
    {
        static DoubleLinkedList GetPhonebook(string filepath)
        {
            DoubleLinkedList pbook = new DoubleLinkedList();
            List<string> list = new List<string>();
            pbook.ReadFile(filepath, list);
            if (new FileInfo(filepath).Length == 0)
            {
                Console.WriteLine("File Danh bạ trống. Nhấn [Enter] để thêm liên lạc");
                Console.ReadKey();
            }
            else
            {
                try
                {
                    pbook.AddToPhonebook(list, pbook);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return pbook;
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Khai báo biến
            string end = "THOÁT DANH BẠ THÀNH CÔNG";
            string filepath = "DanhBa.txt";
            string recyclePath = "Bin.txt";
            string logPath = "Logs.txt";
            string privatePath = "Private.txt";

            // Tất cả các cấu trúc được sử dụng
            DoubleLinkedList pbook = GetPhonebook(filepath);
            DoubleLinkedList privateBook = pbook.GetPrivateSector(privatePath);
            List<Phonebook> recycleBin = pbook.GetRecycleBin(recyclePath);
            List<Phonebook> callLogs = pbook.GetCallLogs(logPath);

        //MENU
        MENU:
            Console.Clear();

            string DoAn = "ĐỒ ÁN: CẤU TRÚC DỮ LIỆU VÀ GIẢI THUẬT";
            string InfoSV = "NHÓM 4 | LỚP: 21C1INF50900703";
            string Separator = "*****************************";

            Console.WriteLine();
            Console.SetCursorPosition((Console.WindowWidth - DoAn.Length) / 2, Console.CursorTop);
            Console.WriteLine(DoAn);
            Console.SetCursorPosition((Console.WindowWidth - InfoSV.Length) / 2, Console.CursorTop);
            Console.WriteLine(InfoSV);
            Console.WriteLine();
            Console.SetCursorPosition((Console.WindowWidth - Separator.Length) / 2, Console.CursorTop);
            Console.WriteLine(Separator);
            Console.WriteLine();

            Console.WriteLine("[0]  ĐÓNG DANH BẠ\n");
            Console.WriteLine("[1]  THÊM LIÊN LẠC VÀO DANH BẠ");
            Console.WriteLine("[2]  XÓA 1 LIÊN LẠC");
            Console.WriteLine("[3]  CHỈNH SỬA THÔNG TIN LIÊN LẠC");
            Console.WriteLine("[4]  IN RA DANH BẠ HIỆN CÓ");
            Console.WriteLine("[5]  SẮP XÉP");
            Console.WriteLine("[6]  TÌM KIẾM");
            Console.WriteLine("[7]  THÙNG RÁC");
            Console.WriteLine("[8]  GỌI ĐIỆN");
            Console.WriteLine("[9]  NHẬT KÝ CUỘC GỌI");
            Console.WriteLine("[10] GỬI SMS");
            Console.WriteLine("[11] SAO LƯU");
            Console.WriteLine("[12] DANH BẠ RIÊNG TƯ");
            Console.WriteLine("[13] LỜI NHẮC SINH NHẬT");
            Console.WriteLine();

            Console.Write("\n > Lựa chọn của người dùng: ");
            string choice = Console.ReadLine();

            // PHẦN CHƯƠNG TRÌNH
            switch (choice)
            {
                case "0": goto THEEND;
                case "1": // THÊM LIÊN LẠC
                    {
                        Console.Clear();
                        Console.SetCursorPosition((Console.WindowWidth - "NHẬP LIÊN LẠC".Length) / 2, Console.CursorTop);
                        Console.WriteLine("NHẬP LIÊN LẠC\n");

                        pbook.AddContact();
                        Console.WriteLine("\nLiên lạc được thêm thành công. Nhấn [Enter] để trở về màn hình chính");
                        pbook.Quicksave(filepath, recyclePath, logPath, privatePath, privateBook, recycleBin, callLogs);
                        Console.ReadKey();
                        goto MENU;
                    }
                case "2": // XÓA ĐI 1 SỐ LIÊN LẠC
                    {
                        Console.Clear();
                        Console.SetCursorPosition((Console.WindowWidth - "XÓA LIÊN LẠC".Length) / 2, Console.CursorTop);
                        Console.WriteLine("XÓA LIÊN LẠC\n");

                        pbook.Print();
                        Console.Write("\nNhập ID/Nickname/SĐT/EMail/Facebook URL muốn xóa: ");
                        string forRemove = Console.ReadLine();
                        pbook.Remove(recycleBin, forRemove);
                        Console.WriteLine("\nLiên lạc được xóa thành công. Nhấn [Enter] để trở về màn hình chính");
                        pbook.Quicksave(filepath, recyclePath, logPath, privatePath, privateBook, recycleBin, callLogs);
                        Console.ReadKey();
                        goto MENU;
                    }
                case "3": // CHỈNH SỬA
                    {
                        Console.Clear();
                        Console.SetCursorPosition((Console.WindowWidth - "CHỈNH SỬA".Length) / 2, Console.CursorTop);
                        Console.WriteLine("CHỈNH SỬA\n");

                        pbook.UpdatePhone();
                        Console.WriteLine("\nChỉnh sửa thành công. Nhấn [Enter] để trở về màn hình chính");
                        pbook.Quicksave(filepath, recyclePath, logPath, privatePath, privateBook, recycleBin, callLogs);
                        Console.ReadKey();
                        goto MENU;
                    }
                case "4": // IN PHONEBOOK VÀ HỎI XEM CÓ MUỐN CHỈNH SỬA KHÔNG
                    {
                    BEGINCASE3:
                        Console.Clear();
                        Console.SetCursorPosition((Console.WindowWidth - "DANH BẠ".Length) / 2, Console.CursorTop);
                        Console.WriteLine("DANH BẠ\n");

                        pbook.Print();
                        if (new FileInfo(filepath).Length != 0)
                        {
                            Console.Write("\nBạn có muốn chỉnh sửa liên lạc nào không? (Y/N) ");
                            string modifyConfirm = Console.ReadLine();
                            if (modifyConfirm.ToLower() == "y")
                            {
                                pbook.UpdatePhone();
                                Console.WriteLine("\nChỉnh sửa thành công. Nhấn [Enter] để trở về màn hình chính");
                                pbook.Quicksave(filepath, recyclePath, logPath, privatePath, privateBook, recycleBin, callLogs);
                                Console.ReadKey();
                            }
                            else if (modifyConfirm.ToLower() != "n")
                            {
                                Console.WriteLine("Lệnh không hợp lệ. Nhấn [Enter]");
                                Console.ReadKey();
                                goto BEGINCASE3;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Danh bạ rỗng. Nhấn [Enter] để bắt đầu thêm vào");
                            Console.ReadKey();
                        }
                        pbook.Quicksave(filepath, recyclePath, logPath, privatePath, privateBook, recycleBin, callLogs);
                        Console.WriteLine("Nhấn [Enter] để trở về màn hình chính");
                        goto MENU;
                    }
                case "5": // SẮP XẾP
                    {
                        Console.Clear();
                        Console.SetCursorPosition((Console.WindowWidth - "SẮP XẾP".Length) / 2, Console.CursorTop);
                        Console.WriteLine("SẮP XẾP\n");
                        pbook.Sort(pbook);
                        Console.WriteLine("\nSắp xếp thành công. Nhấn [Enter] để trở về màn hình chính");
                        pbook.Quicksave(filepath, recyclePath, logPath, privatePath, privateBook, recycleBin, callLogs);
                        Console.ReadKey();
                        goto MENU;
                    }
                case "6": // TÌM KIẾM
                    {
                        Console.Clear();
                        Console.SetCursorPosition((Console.WindowWidth - "TÌM KIẾM".Length) / 2, Console.CursorTop);
                        Console.WriteLine("TÌM KIẾM\n");
                        pbook.FindPlus();
                        Console.WriteLine("\nNhấn [Enter] để trở về màn hình chính");
                        pbook.Quicksave(filepath, recyclePath, logPath, privatePath, privateBook, recycleBin, callLogs);
                        Console.ReadKey();
                        goto MENU;
                    }
                case "7": //THÙNG RÁC
                    {
                        Console.Clear();
                        Console.SetCursorPosition((Console.WindowWidth - "THÙNG RÁC".Length) / 2, Console.CursorTop);
                        Console.WriteLine("THÙNG RÁC\n");

                        pbook.PrintBin(recycleBin, pbook, recyclePath);
                        pbook.Quicksave(filepath, recyclePath, logPath, privatePath, privateBook, recycleBin, callLogs);
                        Console.ReadKey();
                        goto MENU;
                    }
                case "8": // GỌI ĐIỆN
                    {
                        Console.Clear();
                        Console.SetCursorPosition((Console.WindowWidth - "GỌI ĐIỆN".Length) / 2, Console.CursorTop);
                        Console.WriteLine("GỌI ĐIỆN\n");
                        pbook.Call(pbook, callLogs);
                        Console.WriteLine("\nNhấn [Enter] để trở về màn hình chính");
                        pbook.Quicksave(filepath, recyclePath, logPath, privatePath, privateBook, recycleBin, callLogs);
                        Console.ReadKey();
                        goto MENU;
                    }
                case "9": // NHẬT KÝ
                    {
                        pbook.CallLogs(callLogs);
                        Console.WriteLine("\nNhấn [Enter] để trở về màn hình chính");
                        pbook.Quicksave(filepath, recyclePath, logPath, privatePath, privateBook, recycleBin, callLogs);
                        Console.ReadKey();
                        goto MENU;
                    }
                case "10": // GỬI SMS
                    {
                        Console.Clear();
                        Console.SetCursorPosition((Console.WindowWidth - "GỬI SMS".Length) / 2, Console.CursorTop);
                        Console.WriteLine("GỬI SMS\n");
                        pbook.SendSMS();
                        Console.WriteLine("\nNhấn [Enter] để trở về màn hình chính");
                        pbook.Quicksave(filepath, recyclePath, logPath, privatePath, privateBook, recycleBin, callLogs);
                        Console.ReadKey();
                        goto MENU;
                    }
                case "11": // BACKUP
                    {
                        Console.Clear();
                        Console.SetCursorPosition((Console.WindowWidth - "SAO LƯU VÀ KHÔI PHỤC".Length) / 2, Console.CursorTop);
                        Console.WriteLine("SAO LƯU VÀ KHÔI PHỤC\n");
                        pbook.BaR();
                        Console.WriteLine("\nNhấn [Enter] để trở về màn hình chính");
                        Console.ReadKey();
                        goto MENU;
                    }
                case "12": // Private phonebook
                    {
                        pbook.PrivateSector(pbook, privateBook);
                        Console.WriteLine("\nNhấn [Enter] để trở về màn hình chính");
                        pbook.Quicksave(filepath, recyclePath, logPath, privatePath, privateBook, recycleBin, callLogs);
                        Console.ReadKey();
                        goto MENU;
                    }
                case "13": // BDAY
                    {
                        Console.Clear();
                        Console.SetCursorPosition((Console.WindowWidth - "CHÚC MỪNG SINH NHẬT".Length) / 2, Console.CursorTop);
                        Console.WriteLine("CHÚC MỪNG SINH NHẬT\n");
                        pbook.HPBD(pbook);
                        Console.WriteLine("\nNhấn [Enter] để trở về màn hình chính");
                        pbook.Quicksave(filepath, recyclePath, logPath, privatePath, privateBook, recycleBin, callLogs);
                        Console.ReadKey();
                        goto MENU;
                    }
                case "input":
                    {
                        pbook.Add(new Phonebook("001", "HanHan", "0924572124", "hanhan@gmail.com", "www.facebook.com/hanho", "10/08/2002"));
                        pbook.Add(new Phonebook("002", "NganNgan", "0935050924", "bichngan@gmail.com", "www.facebook.com/bichngan", "19/12/2002"));
                        pbook.Add(new Phonebook("003", "QuangQuang", "0901480516", "nhatquang@gmail.com", "www.facebook.com/quangquang", "23/02/2002"));
                        pbook.Add(new Phonebook("004", "TrinhTrinh", "0901169496", "dieutrinh@gmail.com", "www.facebook.com/dieutrinh", "02/08/2002"));
                        pbook.Add(new Phonebook("005", "ThienY", "0971967636", "nhuyotro@gmail.com", "www.facebook.com/nhuyotro", "25/09/2002"));
                        pbook.Quicksave(filepath, recyclePath, logPath, privatePath, privateBook, recycleBin, callLogs);
                        goto MENU;
                    }
                default: Console.WriteLine("\n Lựa chọn không nằm trong menu. [Enter] để lựa chọn lại"); Console.ReadKey(); goto MENU;
            }

        THEEND:
            pbook.Quicksave(filepath, recyclePath, logPath, privatePath, privateBook, recycleBin, callLogs);
            Console.Clear();
            Console.WriteLine();
            Console.SetCursorPosition((Console.WindowWidth - end.Length) / 2, Console.CursorTop);
            Console.WriteLine(end);
            Console.SetCursorPosition(Console.WindowWidth / 2, Console.CursorTop);
            Console.ReadKey();
        }
    }
}
