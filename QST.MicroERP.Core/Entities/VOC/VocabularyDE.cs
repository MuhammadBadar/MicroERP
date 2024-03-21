using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Core.Entities.VOC
{
    public class VocabularyDE : BaseDomain


    {
        public VocabularyDE()
        {
            UserVocab = new UserVocabularyDE();
        }
        public string? Word { get; set; }
        public string? EnglishMeaning { get; set; }
        public string? UrduMeaning { get; set; }
        public string? UserId { get; set; }
        public int NovelId { get; set; }
        public UserVocabularyDE UserVocab { get; set; }
    }
}
