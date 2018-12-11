using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace professorMarketWebUI.Models
{
    public class TutorFinderModel
    {
        public bool NextExist { get; set; }

        public List<BLL.DTO.TutorDTO> Tutors { get; set; }

        public List<BLL.DTO.LessonTypesDTO> types;
        public List<BLL.DTO.SubjectsDTO> subjects;
        public List<BLL.DTO.SubjectTypesDTO> sTypes;

        public int selectedExperience;
        public long selectedSType;        
        public long selectedSubject;        
        public long selectedType;
        public long selectedLevel;
        public int selectedCost;
    }
}