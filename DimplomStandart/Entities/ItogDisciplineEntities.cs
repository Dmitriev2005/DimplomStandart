﻿using DimplomStandart.Windows.DataReference.StudentWindows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimplomStandart.Entities
{
    public class ItogDisciplineEntities
    {
        public ItogDisciplineEntities(string id = "", string idDiscipline = "", string countHour = "",
            string score = "", string idStudent = "", string moreInfo = "", string typeOfActivity = "", string useMethodsEducation = "", string placePassage = "", 
            bool isCourse = false)
        {
            Id = id;
            IdDiscipline = idDiscipline;
            IdStudent = idStudent;
            CountHour = countHour;
            Score = score;
            MoreInfo = moreInfo;
            TypeOfActivity = typeOfActivity;
            UseMethodsEducation = useMethodsEducation;
            PlacePassage = placePassage;
            IsCourseWork = isCourse;
        }

        public string Id { get; set; }
        public string IdDiscipline { get; set; }
        public string IdStudent { get; set; }
        public string Score { get; set; }
        public string MoreInfo { get; set; }
        public string TypeOfActivity { get; set; }
        public string UseMethodsEducation { get; set; }
        public string PlacePassage { get; set; }
        public bool IsCourseWork { get; set; }
        ////////////////////  ///////////////
        public string IdDisciplineStr {
            get
            {
                if (Id != "")
                    return (from q in App.disciplines where q.Id == IdDiscipline select q.Name).ToList().Single();
                else
                    return "";

            }
            set
            {

            }
        }
        public string CountHour {
            get
            {
                if (Id != "")
                    return (from q in App.disciplines where q.Id == IdDiscipline select q.CountHour).ToList().Single();
                else
                    return "";

            }
            set { }
        }
        public string Type
        {
            get
            {
                if (Id != "")
                    return (from q in App.disciplines where q.Id == IdDiscipline select q.Type).ToList().Single();
                else
                    return "";
            }
            set { }

        }
        public string IdStudentStr
        {
            get
            {
                if (Id != "")
                {
                    StudentEntities student = (from q in App.students where q.Id == IdStudent select q).ToList().Single();
                    return student.SurnameIm + " " + student.NameIm + " " + student.LastNameIm;
                }
                else
                    return "";

            }
            set { }
        }
        public string IsCourseWorkStr
        {
            get => IsCourseWork ? "Да" : "Нет";
            set { }
        }

    }
}
