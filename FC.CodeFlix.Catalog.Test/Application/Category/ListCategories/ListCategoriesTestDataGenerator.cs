using FC.CodeFlix.Catalog.Application.UsesCases.Category.ListCategories;

namespace FC.CodeFlix.Catalog.Test.Application.Category.ListCategories;

public class ListCategoriesTestDataGenerator
{
    public static IEnumerable<object[]> GetInputsWithoutAllParameter(int times = 14)
    {
        var fixture = new ListCategoriesTestFixture();
        var inputExample = fixture.GetExempleInput();
        for (int i = 0; i < times; i++)
        {
            switch (i % 7)
            {
                case 0:
                    yield return new object[] { new ListCategoriesInput() };
                    break;
                case 1:
                    yield return new object[] { new ListCategoriesInput(inputExample.Page) };
                    break;
                case 2:
                    yield return new object[] { new ListCategoriesInput(inputExample.Page, inputExample.PerPage) };
                    break;
                case 3:
                    yield return new object[]
                        { new ListCategoriesInput( inputExample.Page,inputExample.PerPage,inputExample.Search) };
                    break;
                case 4:
                    yield return new object[]
                    {
                        new ListCategoriesInput(inputExample.Page,
                             inputExample.PerPage, inputExample.Search,inputExample.Sort)
                    };
                    break;
                case 5:
                    yield return new object[]
                    {
                        inputExample
                    };
                    break;
                default:
                    yield return new object[] { new ListCategoriesInput() };
                    break;
            }
        }
    }
}