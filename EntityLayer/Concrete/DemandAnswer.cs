using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class DemandAnswer
    {
        [Key]
        public int DemandAnswersId { get; set; }
        public int DemandId { get; set; }
        public Demand Demand { get; set; }
        //Talep tarihi ile sıralama yaparak mesajları yeniden eskiye doğru gösterebilmek mümkün.
        public DateTime  DemandAnswerDate { get; set; }
        //Eğer talep yanıt tipi 1 ise kullanıcı yazıyor, eğer 2 ise admin veya personel yazıyordur. böylelikle chat'te süreklilik söz konusu olabiliyor.
        public int DemandAnswerType { get; set; }
        public string Answer { get; set; }
    }
}
