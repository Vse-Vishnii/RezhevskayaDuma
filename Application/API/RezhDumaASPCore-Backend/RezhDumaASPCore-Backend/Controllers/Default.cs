using System.Collections.Generic;
using RezhDumaASPCore_Backend.Model;

namespace RezhDumaASPCore_Backend.Controllers
{
    public static class Default
    {
        private static User user;
        private static District d1;
        private static District d2;
        private static Category category;
        private static User deputy;
        private static User deputy2;

        public static void CreateData(UserContext db)
        {
            user = CreateUser(db);
            CreateFirstDeputy(db);
            CreateSecondDeputy(db);
            CreateApplication(db);
        }

        private static void CreateApplication(UserContext db)
        {
            var application = new Application
            {
                Applicant = user,
                Name = "Application 1",
                Description = "It's a test application with sent status",
                Status = Status.Sent,
                Districts = new List<District> {d1, d2},
                Categories = new List<Category>{category},
                Deputy = deputy
            };
            db.Add(new DeputyApplication(application, deputy));
            db.Add(application);
            application = new Application
            {
                Applicant = user,
                Name = "Application 4",
                Description = "It's a test application with sent status",
                Status = Status.Sent,
                Districts = new List<District> { d1, d2 },
                Categories = new List<Category> { category },
                Deputy = deputy
            };
            db.Add(new DeputyApplication(application, deputy));
            db.Add(application);
            application = new Application
            {
                Applicant = user,
                Name = "Application 2",
                Description = "It's a test application with process status",
                Status = Status.InProcess,
                Categories = new List<Category> { category },
                Deputy = deputy2
            };
            db.Add(new DeputyApplication(application, deputy));
            db.Add(application);
            application = new Application
            {
                Applicant = user,
                Name = "Application 3",
                Description = "It's a test application with done status",
                Status = Status.Done,
                Deputy = deputy2
            };
            db.Add(new DeputyApplication(application, deputy));
            db.Add(application);
            db.SaveChanges();
        }

        private static void CreateSecondDeputy(UserContext db)
        {
            deputy2 = new User
            {
                Firstname = "Депутат 2",
                Role = Role.Deputy,
                Password = "123"
            };
            db.Add(deputy2);
            d1 = new District
            {
                Name = "Округ 2",
                Deputy = deputy2
            };
            db.Districts.Add(d1);
            db.SaveChanges();
        }

        private static void CreateFirstDeputy(UserContext db)
        {
            deputy = new User
            {
                Surname = "Шарова",
                Role = Role.Deputy,
                Password = "123"
            };
            db.Add(deputy);
            category = new Category
            {
                Name = "ПО ПРОМЫШЛЕННОСТИ,СТРОИТЕЛЬСТВУ,КОМ.ХОЗ.,ТРАНСПОРТУ И СВЯЗИ",
                Deputy = deputy
            };
            db.Add(category);
            d2 = new District
            {
                Name = "Округ 1",
                Deputy = deputy
            };
            db.Districts.Add(d2);
            db.SaveChanges();
        }

        private static User CreateUser(UserContext db)
        {
            var user = new User
            {
                Firstname = "Алексей",
                Role = Role.Applicant,
                Password = "123"
            };
            db.Add(user);
            db.SaveChanges();
            return user;
        }
    }
}
