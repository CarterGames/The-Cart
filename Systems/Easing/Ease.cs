namespace Scarlet.Easing
{
    public class Ease
    {
        public static double ReadValue(EaseData data, double current) => ReadValue(data.easeType, current);
        public static double ReadValue(EaseType type, double current)
        {
            switch (type)
            {
                case EaseType.None:
                    return current;
                case EaseType.Linear:
                    return EaseHandler.Linear(current);
                case EaseType.InSine:
                    return EaseHandler.EaseInSine(current);
                case EaseType.InCubic:
                    return EaseHandler.EaseInCubic(current);
                case EaseType.InQuint:
                    return EaseHandler.EaseInQuint(current);
                case EaseType.InCirc:
                    return EaseHandler.EaseInCirc(current);
                case EaseType.InElastic:
                    return EaseHandler.EaseInElastic(current);
                case EaseType.InQuad:
                    return EaseHandler.EaseInQuad(current);
                case EaseType.InQuart:
                    return EaseHandler.EaseInQuart(current);
                case EaseType.InExpo:
                    return EaseHandler.EaseInExpo(current);
                case EaseType.InBack:
                    return EaseHandler.EaseInBack(current);
                case EaseType.InBounce:
                    return EaseHandler.EaseInBounce(current);
                case EaseType.OutSine:
                    return EaseHandler.EaseOutSine(current);
                case EaseType.OutCubic:
                    return EaseHandler.EaseOutCubic(current);
                case EaseType.OutQuint:
                    return EaseHandler.EaseOutQuint(current);
                case EaseType.OutCirc:
                    return EaseHandler.EaseOutCirc(current);
                case EaseType.OutElastic:
                    return EaseHandler.EaseOutElastic(current);
                case EaseType.OutQuad:
                    return EaseHandler.EaseOutQuad(current);
                case EaseType.OutQuart:
                    return EaseHandler.EaseOutQuart(current);
                case EaseType.OutExpo:
                    return EaseHandler.EaseOutExpo(current);
                case EaseType.OutBack:
                    return EaseHandler.EaseOutBack(current);
                case EaseType.OutBounce:
                    return EaseHandler.EaseOutBounce(current);
                case EaseType.InOutSine:
                    return EaseHandler.EaseInOutSine(current);
                case EaseType.InOutCubic:
                    return EaseHandler.EaseInOutCubic(current);
                case EaseType.InOutQuint:
                    return EaseHandler.EaseInOutQuint(current);
                case EaseType.InOutCirc:
                    return EaseHandler.EaseInOutCirc(current);
                case EaseType.InOutElastic:
                    return EaseHandler.EaseInOutElastic(current);
                case EaseType.InOutQuad:
                    return EaseHandler.EaseInOutQuad(current);
                case EaseType.InOutQuart:
                    return EaseHandler.EaseInOutQuart(current);
                case EaseType.InOutExpo:
                    return EaseHandler.EaseInOutExpo(current);
                case EaseType.InOutBack:
                    return EaseHandler.EaseInOutBack(current);
                case EaseType.InOutBounce:
                    return EaseHandler.EaseInOutBounce(current);
                default:
                    return current;
            }
        }

        public static double ReadValue(InEaseData type, double current) => ReadValue(type.easeType, true, current);
        public static double ReadValue(OutEaseData type, double current) => ReadValue(type.easeType, false, current);
        
        public static double ReadValue(SimpleEaseType type, bool isIn, double current)
        {
            switch (type)
            {
                case SimpleEaseType.None:
                    return current;
                case SimpleEaseType.Linear:
                    return EaseHandler.Linear(current);
                case SimpleEaseType.Sine:
                    return isIn ? EaseHandler.EaseInSine(current) : EaseHandler.EaseOutSine(current);
                case SimpleEaseType.Cubic:
                    return isIn ? EaseHandler.EaseInCubic(current) : EaseHandler.EaseOutCubic(current);
                case SimpleEaseType.Quint:
                    return isIn ? EaseHandler.EaseInQuint(current) : EaseHandler.EaseOutQuint(current);
                case SimpleEaseType.Circ:
                    return isIn ? EaseHandler.EaseInCirc(current) : EaseHandler.EaseOutCirc(current);
                case SimpleEaseType.Elastic:
                    return isIn ? EaseHandler.EaseInElastic(current) : EaseHandler.EaseOutElastic(current);
                case SimpleEaseType.Quad:
                    return isIn ? EaseHandler.EaseInQuad(current) : EaseHandler.EaseOutQuad(current);
                case SimpleEaseType.Quart:
                    return isIn ? EaseHandler.EaseInQuart(current) : EaseHandler.EaseOutQuart(current);
                case SimpleEaseType.Expo:
                    return isIn ? EaseHandler.EaseInExpo(current) : EaseHandler.EaseOutExpo(current);
                case SimpleEaseType.Back:
                    return isIn ? EaseHandler.EaseInBack(current) : EaseHandler.EaseOutBack(current);
                case SimpleEaseType.Bounce:
                    return isIn ? EaseHandler.EaseInBounce(current) : EaseHandler.EaseOutBounce(current);
                default:
                    return current;
            }
        }
    }
}