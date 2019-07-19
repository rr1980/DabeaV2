namespace DabeaV2.ViewModels
{
    public class BenutzerValidationResultModel
    {
        public string ErrMsg { get; set; }
        public bool Fail { get; set; }

        public UserViewvModel Benutzer { get; set; }
    }
}
