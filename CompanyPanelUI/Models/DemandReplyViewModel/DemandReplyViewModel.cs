using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace CompanyPanelUI.Models.DemandReplyViewModel
{
    public class DemandReplyViewModel
    {
        public List<DemandAndAnswerWm> DemandAndAnswerWmList { get; set; }
        public string DemandAnswerNew { get; set; }
        public int DemandIdNew { get; set; }
    }
    public class DemandAndAnswerWm
    {
        public string UserId { get; set; }
        public int ServiceId { get; set; }
        public string DemandTitle { get; set; }
        public string UserName { get; set; }
        public string DemandContent { get; set; }
        public DateTime DemantCreateDate { get; set; }
        public List<DemandFile> DemandFilePath{ get; set; }
        public bool DemandStatus { get; set; }
        public int DemandId { get; set; }
        public DateTime DemandAnswerDate { get; set; }
        public string DemandAnswer { get; set; }

    }
}
