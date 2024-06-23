namespace MediCompendium.Models.ApiRecords;

public record ActiveIngredients {
   public string name { get; set; }
   public string strength { get; set; }
}

public record Packaging {
   public string package_ndc { get; set; }
   public string description { get; set; }
   public string marketing_start_date { get; set; }
   public bool sample { get; set; }
}

public record OpenFda {
   public List<string> manufacturer_name { get; set; }
   public List<string> spl_set_id { get; set; }
   public List<bool> is_original_packager { get; set; }
   public List<string> upc { get; set; }
   public List<string> unii { get; set; }
}

public record NdcData {
   public string product_ndc { get; set; }
   public string generic_name { get; set; }
   public string labeler_name { get; set; }
   public string brand_name { get; set; }
   public List<ActiveIngredients> active_ingredients { get; set; }
   public bool finished { get; set; }
   public List<Packaging> packaging { get; set; }
   public string listing_expiration_date { get; set; }
   public OpenFda openfda { get; set; }
   public string marketing_category { get; set; }
   public string dosage_form { get; set; }
   public string spl_id { get; set; }
   public string product_type { get; set; }
   public List<string> route { get; set; }
   public string marketing_start_date { get; set; }
   public string product_id { get; set; }
   public string application_number { get; set; }
   public string brand_name_base { get; set; }
}