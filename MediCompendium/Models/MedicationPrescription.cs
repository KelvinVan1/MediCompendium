namespace MediCompendium.Models;

public class MedicationPrescription : Medication {
    public string? DeaSchedule { get; set; }
    public List<string>? Description { get; set; }
    public List<string>? HowSupplied { get; set; }
    public List<string>? WarningsAndCautions { get; set; }
}