using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
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

        private static Application app1;
        private static Application app2;
        private static Application app3;
        private static Application app4;

        private static PasswordHasher<User> passwordHasher = new PasswordHasher<User>();

        public static void CreateData(UserContext db)
        {
            user = CreateUser(db);
            CreateFirstDeputy(db);
            CreateSecondDeputy(db);
            CreateApplication(db);
            CreateAnswer(db);
        }

        private static void CreateApplication(UserContext db)
        {
            app1 = new Application
            {
                Applicant = user,
                Name = "Application 1",
                Description = "It's a test application with sent status",
                Status = Status.Sent,
                Districts = new List<District> {d1, d2},
                Categories = new List<Category>{category},
                Deputy = deputy,
                Created = DateTime.Now
            };
            db.Add(new DeputyApplication(app1, deputy));
            db.Add(app1);
            app2 = new Application
            {
                Applicant = user,
                Name = "Application 4",
                Description = "It's a test application with sent status",
                Status = Status.Sent,
                Districts = new List<District> { d1, d2 },
                Categories = new List<Category> { category },
                Deputy = deputy,
                Created = DateTime.Now
            };
            db.Add(new DeputyApplication(app2, deputy));
            db.Add(app2);
            app3 = new Application
            {
                Applicant = user,
                Name = "Application 2",
                Description = "It's a test application with process status",
                Status = Status.InProcess,
                Categories = new List<Category> { category },
                Deputy = deputy2,
                Created = DateTime.Now
            };
            db.Add(new DeputyApplication(app3, deputy));
            db.Add(app3);
            app4 = new Application
            {
                Applicant = user,
                Name = "Application 3",
                Description = "It's a test application with done status",
                Status = Status.Done,
                Deputy = deputy2,
                Created = DateTime.Now
            };
            db.Add(new DeputyApplication(app4, deputy));
            db.Add(app4);
            db.SaveChanges();
        }

        private static void CreateAnswer(UserContext db)
        {
            var answer = new Answer()
            {
                ApplicationId = app4.Id,
                Name = "Answer",
                Description = "I test answers",
                Created = DateTime.Now
            };
            db.Add(answer);
            answer = new Answer()
            {
                ApplicationId = app3.Id,
                Name = "Answer",
                Description = "Check problem",
                Created = DateTime.Now
            };
            db.Add(answer);
            db.SaveChanges();
        }

        private static void CreateSecondDeputy(UserContext db)
        {
            deputy2 = new User
            {
                Firstname = "Депутат 2",
                Role = Role.Deputy,
                Password = passwordHasher.HashPassword(deputy2,"123"),
                Email = "deputy@email.ru"
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
                Password = passwordHasher.HashPassword(deputy, "123"),
                Email = "sharova@email.ru"
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
                Email = "user@email.ru"
            };
            user.Password = passwordHasher.HashPassword(user, "123");
            db.Add(user);
            db.SaveChanges();
            return user;
        }
    }
}
