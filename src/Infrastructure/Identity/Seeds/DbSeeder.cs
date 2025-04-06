using Core.Domain.Entities;
using Infrastructure.Identity.Models;
using Infrastructure.Persistence.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Identity.Seeds
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Asegurar que existen los roles
            if (!await roleManager.RoleExistsAsync("Teacher"))
                await roleManager.CreateAsync(new IdentityRole("Teacher"));

            // Crear 5 usuarios y profesores
            var teachers = new List<Teacher>();
            for (int i = 1; i <= 5; i++)
            {
                string email = $"teacher{i}@school.com";
                var user = await userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    user = new AppUser
                    {
                        UserName = email,
                        Email = email
                    };
                    await userManager.CreateAsync(user, "Password123!");
                    await userManager.AddToRoleAsync(user, "Teacher");
                }

                // Verificar si el Teacher ya existe
                if (!context.Teachers.Any(t => t.UserId == user.Id))
                {
                    teachers.Add(new Teacher
                    {
                        Id = Guid.NewGuid(),
                        Name = $"Teacher {i}",
                        UserId = user.Id
                    });
                }
            }

            if (teachers.Any())
            {
                context.Teachers.AddRange(teachers);
                await context.SaveChangesAsync();
            }

            // Obtener IDs de los profesores insertados
            var teacherIds = context.Teachers.Select(t => t.Id).ToList();

            // Crear 10 materias, 2 por profesor
            var subjects = new List<Subject>();
            int subjectCount = 1;
            foreach (var teacherId in teacherIds)
            {
                for (int i = 0; i < 2; i++)
                {
                    subjects.Add(new Subject
                    {
                        Id = Guid.NewGuid(),
                        Name = $"Subject {subjectCount++}",
                        Credits = 3,
                        TeacherId = teacherId
                    });
                }
            }

            if (!context.Subjects.Any())
            {
                context.Subjects.AddRange(subjects);
                await context.SaveChangesAsync();
            }

            // Crear 5 usuarios y estudiantes
            var students = new List<Student>();
            for (int i = 1; i <= 5; i++)
            {
                string email = $"student{i}@school.com";
                var user = await userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    user = new AppUser
                    {
                        UserName = email,
                        Email = email
                    };
                    await userManager.CreateAsync(user, "Password123!");
                    await userManager.AddToRoleAsync(user, "Student");
                }

                if (!context.Students.Any(s => s.UserId == user.Id))
                {
                    students.Add(new Student
                    {
                        Id = Guid.NewGuid(),
                        Name = $"Student {i}",
                        UserId = user.Id
                    });
                }
            }

            if (students.Any())
            {
                context.Students.AddRange(students);
                await context.SaveChangesAsync();
            }
        }
    }
}
