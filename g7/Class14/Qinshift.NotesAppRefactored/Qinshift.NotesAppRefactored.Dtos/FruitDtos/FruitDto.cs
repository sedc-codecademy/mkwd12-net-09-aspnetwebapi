namespace Qinshift.NotesAppRefactored.Dtos.FruitDtos
{
    public class FruitDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genus { get; set; }
        public string Family { get; set; }
        public string Order { get; set; }
        public NutritionDto? Nutritions { get; set; }
    }
}
