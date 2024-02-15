using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroERP.Core.Entities.VOC
{
    public class UserVocabularyDE : BaseDomain
    {
        public int WordId { get; set; }

        public string? Word { get; set; }
        public string? UserId { get; set; }
        public string? User { get; set; }
        public string? Pronunciation { get; set; }
        public string? Sentence { get; set; }
        public int? VocabDifficultyLevelId { get; set; }
        public string? DifficultyLevel { get; set; }
        public int? NovelId { get; set; }
        public string? Novel { get; set; }
        public int? ChapterId { get; set; }
        public string? Chapters { get; set; }
        public string? Comments { get; set; }
        public bool? IsNeedHelp { get; set; }
    }
}
