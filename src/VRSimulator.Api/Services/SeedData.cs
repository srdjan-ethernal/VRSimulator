using VRSimulator.Api.Domain;

namespace VRSimulator.Api.Services;

public static class SeedData
{
    public static List<TrainingScenario> CreateScenarios()
    {
        return new List<TrainingScenario>
        {
            new(
                Guid.Parse("c77fe939-98ee-4ca5-9f61-0d8f9d31f2b2"),
                "fire-protection",
                "Protivpozarna zastita",
                "Fire protection",
                "Prepoznavanje rizika, evakuacija, izbor opreme i pravilno reagovanje.",
                "Risk recognition, evacuation, equipment selection, and correct response.",
                "Safety",
                35),
            new(
                Guid.Parse("0d91384d-6c32-4f84-89c6-2bc378d01c18"),
                "radioactive-materials",
                "Upravljanje radioaktivnim materijalima",
                "Radioactive material handling",
                "Zastitna oprema, kontrola izlozenosti i proceduralne provere.",
                "Protective equipment, exposure control, and procedural checks.",
                "Hazardous materials",
                45),
            new(
                Guid.Parse("bf90d0a8-f907-40fc-9450-bc6ec237bcb9"),
                "chemical-waste",
                "Upravljanje hemijskim otpadom",
                "Chemical waste management",
                "Klasifikacija, obelezavanje, skladistenje i reakcija na prosipanje.",
                "Classification, labeling, storage, and spill response.",
                "Waste management",
                40),
            new(
                Guid.Parse("ffdb0364-47e2-4f4d-96b3-38675ba0938a"),
                "construction-waste",
                "Upravljanje gradjevinskim otpadom",
                "Construction waste management",
                "Razdvajanje materijala, bezbedan rad i pracenje tokova otpada.",
                "Material sorting, safe work, and waste flow tracking.",
                "Waste management",
                30),
            new(
                Guid.Parse("c391aa43-b3c6-4876-93db-fca11143e2b5"),
                "electronic-waste",
                "Upravljanje elektronskim otpadom",
                "Electronic waste management",
                "Sortiranje uredjaja, baterija, kablova i osetljivih komponenti.",
                "Sorting devices, batteries, cables, and sensitive components.",
                "Waste management",
                35),
            new(
                Guid.Parse("2a2d3685-1488-4d53-bd78-b82bb3a4c0d0"),
                "biomedical-waste",
                "Upravljanje biomedicinskim otpadom",
                "Biomedical waste management",
                "Razdvajanje, pakovanje i privremeno skladistenje zdravstvenog otpada.",
                "Separation, packaging, and temporary storage of healthcare waste.",
                "Healthcare safety",
                40)
        };
    }

    public static List<Course> CreateCourses(IReadOnlyCollection<TrainingScenario> scenarios)
    {
        return scenarios
            .Select(scenario => new Course(
                Guid.NewGuid(),
                scenario.Code,
                scenario.NameSr,
                scenario.NameEn,
                scenario.DescriptionSr,
                scenario.DescriptionEn,
                12,
                80,
                new[] { scenario.Id }))
            .ToList();
    }
}

