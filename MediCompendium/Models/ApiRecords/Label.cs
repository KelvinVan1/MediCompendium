using System.Text.Json.Serialization;

namespace MediCompendium.Models.ApiRecords;

public record LabelData {
    public List<string>? purpose { get; set; }
    public List<string>? keep_out_of_reach_of_children { get; set; }
    public List<string>? warnings { get; set; }
    public List<string>? when_using { get; set; }
    public List<string>? questions { get; set; }
    public List<string>? dosage_and_administration { get; set; }
    public List<string>? pregnancy_or_breast_feeding { get; set; }
    public List<string>? stop_use { get; set; }
    public List<string>? storage_and_handling { get; set; }
    public List<string>? do_not_use { get; set; }
    public List<string>? indications_and_usage { get; set; }
    public OpenFda? openfda { get; set; }
    
    // Prescription specific fields (Some OTC medications will contain some fields)
    public List<string>? spl_unclassified_section { get; set; }
    public List<string>? spl_unclassified_section_table { get; set; }
    
    [JsonPropertyName("description")]
    public List<string>? Description { get; set; }
    
    [JsonPropertyName("how_supplied")]
    public List<string>? HowSupplied { get; set; }
    
    public List<string>? boxed_warning { get; set; }
    public List<string>? warnings_and_cautions { get; set; } 
    public List<string>? dosage_forms_and_strengths { get; set; } 
    public List<string>? clinical_pharmacology { get; set; }
    public List<string>? clinical_pharmacology_table { get; set; }
    public List<string>? mechamism_of_action { get; set; }
    public List<string>? pharmacodynamics { get; set; } 
    public List<string>? pharmacokinetics { get; set; }
    public List<string>? nonclinical_toxicology { get; set; } 
    public List<string>? microbiology { get; set; }
    public List<string>? microbiology_table { get; set; }
    public List<string>? clinical_studies { get; set; }
    public List<string>? carcinogeneis_and_mutagenesis_and_impairment_of_fertility { get; set; } 
    public List<string>? drug_interactions { get; set; }
    public List<string>? drug_interactions_table { get; set; }
    public List<string>? overdosage { get; set; } 
    public List<string>? use_in_specific_populations { get; set; } 
    public List<string>? contraindictations { get; set; }
    public List<string>? precautions { get; set; }
    public List<string>? general_precautions { get; set; }
    public List<string>? information_for_patients { get; set; }
    public List<string>? spl_medguide { get; set; } 
    public List<string>? pregnancy { get; set; }
    public List<string>? nonteratogenic_effects { get; set; }
    public List<string>? nursing_mothers { get; set; }
    public List<string>? pediatric_use { get; set; }
    public List<string>? geriatric_use { get; set; }
    public List<string>? adverse_reactions { get; set; }
}