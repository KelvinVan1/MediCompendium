namespace MediCompendium.Models;

public class MedicationOTC : Medication{
    public string? GenericName { get; set; }
    public string? DosageForm { get; set; }
    public List<string>? Purpose { get; set; }
    public List<string>? Questions { get; set; }
    public List<string>? KeepOutOfReach { get; set; }
}