using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class Setting
    {
        [Key] 
        public int Id { get; set; }

        public string SettingName { get; set; }
        public string SettingValue { get; set; }
    }
}