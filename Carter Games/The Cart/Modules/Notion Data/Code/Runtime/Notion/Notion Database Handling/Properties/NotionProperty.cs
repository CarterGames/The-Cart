#if CARTERGAMES_CART_MODULE_NOTIONDATA

/*
 * Copyright (c) 2025 Carter Games
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System.Reflection;

namespace CarterGames.Cart.Modules.NotionData
{
    public abstract class NotionProperty
    {
        protected abstract object InternalValue { get; set; }
        public abstract string JsonValue { get; protected set; }


        public NotionPropertyCheckbox CheckBox() => NotionPropertyFactory.Checkbox(InternalValue, JsonValue);
        public NotionPropertyDate Date() => NotionPropertyFactory.Date(InternalValue, JsonValue);
        public NotionPropertyMultiSelect MultiSelect() => NotionPropertyFactory.MultiSelect(InternalValue, JsonValue);
        public NotionPropertySelect Select() => NotionPropertyFactory.Select(InternalValue, JsonValue);
        public NotionPropertyNumber Number() => NotionPropertyFactory.Number(InternalValue, JsonValue);
        public NotionPropertyRichText RichText() => NotionPropertyFactory.RichText(InternalValue, JsonValue);
        public NotionPropertyTitle Title() => NotionPropertyFactory.Title(InternalValue, JsonValue);


        public bool TryConvertValueToType<T>(out T value)
        {
            value = default;
            
            if (typeof(T).BaseType.FullName.Contains(typeof(NotionDataWrapper<>).Namespace + ".NotionDataWrapper"))
            {
                if (NotionPropertyValueHandler.TryGetValueAsWrapper(this, typeof(T), out var valueObj))
                {
                    value = (T) valueObj;
                    return true;
                }
            }
            else
            {
                if (NotionPropertyValueHandler.TryGetValueAs(this, typeof(T), out var valueObj))
                {
                    value = (T) valueObj;
                    return true;
                }
            }

            return false;
        }


        public bool TryConvertValueToFieldType(FieldInfo field, object target)
        {
            var fieldType = field.FieldType;
                        
            if (fieldType.BaseType.FullName.Contains(typeof(NotionDataWrapper<>).Namespace + ".NotionDataWrapper"))
            {
                if (NotionPropertyValueHandler.TryGetValueAsWrapper(this, fieldType, out var value))
                {
                    field.SetValue(target, value);
                    return true;
                }
            }
            else
            {
                if (NotionPropertyValueHandler.TryGetValueAs(this, fieldType, out var value))
                {
                    field.SetValue(target, value);
                    return true;
                }
            }

            return false;
        }
    }
}

#endif