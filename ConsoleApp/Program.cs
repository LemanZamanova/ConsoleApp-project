using ConsoleApp;

List<Group> groups = new();
string input;
do
{
    Console.WriteLine("//////////////////////////////////// ");
    Console.WriteLine("Yeni qrup yarat ucun 1.");
    Console.WriteLine("Qruplarin siyahisini gormek ucun 2.");
    Console.WriteLine("Qrup uzerinde duzelis etmek ucun 3.");
    Console.WriteLine("Qrupdaki telebelerin siyahisini gormek ucun 4.");
    Console.WriteLine("Butun telebelerin siyahisini gormek  ucun 5.");
    Console.WriteLine("Telebe yaratmaq ucun 6.");
    Console.WriteLine("Emeliyyatlardan cixis etmek ucun 0.");
    Console.WriteLine("/////////////////////////////////////");
    Console.WriteLine("Seciminiz: ");
    input = Console.ReadLine();
    switch (input)
    {
        case "1":
            CreateGroup();
            break;
        case "2":
            ShowGroup();
            break;
        case "3":
            EditGroup();
            break;
        case "4":
            ShowStudentInGroup();
            break;
        case "5":
            ShowAllStudents();
            break;
        case "6":
            CreateStudent();
            break;
        case "0":
            Console.WriteLine("Cixis");
            break;
        default:
            Console.WriteLine("Yanlış seçim. Zəhmət olmasa yenidən cəhd edin.");
            break;
    }

} while (input != "0");

void CreateGroup()
{
    Console.WriteLine("Qrup nomresi daxil edin: ");
    string no;
    while (true)
    {
        no = Console.ReadLine();
        bool groupExist = false;
        foreach (Group g in groups)
        {
            if (g.No == no)
            {
                groupExist = true;
                break;
            }
        }
        if (!groupExist)

            break;
        Console.WriteLine("Bu adda qrup movcuddur.Basqa qrup ad daxil edin");
        return;
    }
    Console.WriteLine("Kateqoriya secin: ");
    string category = Console.ReadLine();
    bool isOnline;
    while (true)
    {
        Console.Write("Onlayn qrupdur?(beli ve yaxud xeyr daxil edin): ");
        string onlineInput = Console.ReadLine().Trim().ToLower();

        if (onlineInput == "beli")
        {
            isOnline = true;
            break;
        }
        else if (onlineInput == "xeyr")
        {
            isOnline = false;
            break;
        }
        else
        {
            Console.WriteLine("Yalniz 'beli' və ya 'xeyr' daxil edin.");
        }
    }
    Group group = new Group
    {
        No = no,
        Category = category,
        IsOnline = isOnline

    };
    AddGroup(group);
    Console.WriteLine("Yeni  qrup yaradildi.");
}
void AddGroup(Group group)
{
    groups.Add(group);
}

void ShowGroup()
{
    if (groups.Count == 0)
    {
        Console.WriteLine("Qrup yoxdur");
    }
    else
    {
        foreach (Group g in groups)
        {
            Console.WriteLine($"Qrup nomresi: {g.No}, Kateqoriya: {g.Category}, Telebe sayi: {g.Students.Count}");
        }
    }

}

void EditGroup()
{
    try
    {
        Group groupEdit = null;
        string beforeNo;

        while (groupEdit == null)
        {
            Console.Write("Deyismek istediyiniz qrupun nomresini daxil edin: ");
            beforeNo = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(beforeNo))
            {
                Console.WriteLine("Qrup nomresi bos ola bilmez. Yeniden cehd edin.");
                continue;
            }

            foreach (Group g in groups)
            {
                if (g.No == beforeNo)
                {
                    groupEdit = g;
                    break;
                }
            }

            if (groupEdit == null)
            {
                Console.WriteLine("Bele bir qrup movcud deyil. Yeniden cehd edin.");
            }
        }

        string afterNo;
        bool groupIsExist;

        do
        {
            Console.Write("Yeni qrup nomresi daxil edin: ");
            afterNo = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(afterNo))
            {
                Console.WriteLine("Yeni qrup nomresi bos ola bilmez.");
                groupIsExist = true;
                continue;
            }

            groupIsExist = false;
            foreach (Group g in groups)
            {
                if (g.No == afterNo)
                {
                    groupIsExist = true;
                    break;
                }
            }

            if (groupIsExist)
            {
                Console.WriteLine("Bu adda artiq baska bir qrup movcuddur. Yeniden cehd edin.");
            }

        } while (groupIsExist);

        groupEdit.No = afterNo;

        foreach (Student student in groupEdit.Students)
        {
            student.GroupNum = afterNo;
        }

        Console.WriteLine("Qrup nomresi ugurla deyisdirildi.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Xeta bas verdi: {ex.Message}");
        Console.WriteLine("Zehmet olmasa yeniden cehd edin.");
    }
}


void ShowStudentInGroup()
{
    Console.WriteLine("Qrupun nomresini daxil edin: ");
    string groupNum = Console.ReadLine();
    Group group = groups.FirstOrDefault(g => g.No == groupNum);

    if (group == null)
    {
        Console.WriteLine("Bu adda qrup yoxdur.");
        return;
    }

    if (group.Students.Count == 0)
    {
        Console.WriteLine("Bu qrupda telebe yoxdur.");
    }
    else
    {
        foreach (var student in group.Students)
        {
            Console.WriteLine($"Ad: {student.Name}, Soyad: {student.Surname}, Tip: {student.Type}");
        }
    }
}

void ShowAllStudents()
{
    List<Student> allStudents = new List<Student>();
    foreach (var groupInList in groups)
    {
        allStudents.AddRange(groupInList.Students);
    }

    if (allStudents.Count == 0)
    {
        Console.WriteLine("Telebe yoxdur.");
    }
    else
    {
        foreach (var student in allStudents)
        {
            Console.WriteLine($"Ad: {student.Name}, Soyad: {student.Surname}, Qrup: {student.GroupNum}, Online: {(student.Group.IsOnline ? "Beli" : "Xeyr")}");
        }
    }
}
void CreateStudent()
{
    Console.Write("Ad: ");
    string name = Console.ReadLine();

    Console.Write("Soyad: ");
    string surname = Console.ReadLine();

    Console.Write("Qrup nömresi: ");
    string groupNo = Console.ReadLine();

    var groupForStudent = groups.FirstOrDefault(g => g.No == groupNo);
    if (groupForStudent == null)
    {
        Console.WriteLine("Bele bir qrup yoxdur.");
        return;
    }

    if (groupForStudent.Students.Count >= groupForStudent.Limit)
    {
        Console.WriteLine("Bu qrup doludur!");
        return;
    }

    Console.Write("Tip (zemanetli / zemanetsiz): ");
    string type = Console.ReadLine();

    Student student = new Student
    {
        Name = name,
        Surname = surname,
        GroupNum = groupNo,
        Group = groupForStudent,
        Type = type
    };

    groupForStudent.Students.Add(student);
    Console.WriteLine("Telebe ugurla elave edildi.");
}

