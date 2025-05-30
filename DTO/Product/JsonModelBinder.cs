using bike_store_2.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace bike_store_2.DTO
{

    public class JsonModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueProviderResult == ValueProviderResult.None)
                return Task.CompletedTask;

            var value = valueProviderResult.FirstValue;

            if (string.IsNullOrEmpty(value))
                return Task.CompletedTask;

            try
            {
                var result = JsonConvert.DeserializeObject(value, bindingContext.ModelType);
                bindingContext.Result = ModelBindingResult.Success(result);
            }
            catch
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Invalid JSON.");
            }

            return Task.CompletedTask;
        }
    }



    public class JsonModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType == typeof(Product)) // عدل حسب موديلك
            {
                return new JsonModelBinder();
            }

            return null;
        }

        public IModelBinder? GetBinder(ModelBinderProviderContext bindingContext)
        {
            if (bindingContext.MetadataProvider == typeof(Product)) // عدل حسب موديلك
            {
                return new JsonModelBinder();
            }

            return null;
        }
    }


}