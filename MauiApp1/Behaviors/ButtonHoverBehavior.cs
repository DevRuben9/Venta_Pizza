namespace MauiApp1.Behaviors
{
    class ButtonHoverBehavior : Behavior<Button>
    {
        public static readonly BindableProperty NormalColorProperty =
            BindableProperty.Create(nameof(NormalColor), typeof(Color), typeof(ButtonHoverBehavior), Colors.LightGray);

        public Color NormalColor
        {
            get => (Color)GetValue(NormalColorProperty);
            set => SetValue(NormalColorProperty, value);
        }

        public static readonly BindableProperty HoverColorProperty =
            BindableProperty.Create(nameof(HoverColor), typeof(Color), typeof(ButtonHoverBehavior), Colors.DarkGray);

        public Color HoverColor
        {
            get => (Color)GetValue(HoverColorProperty);
            set => SetValue(HoverColorProperty, value);
        }

        protected override void OnAttachedTo(Button bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.Pressed += OnPressed;
            bindable.Released += OnReleased;
            bindable.BackgroundColor = NormalColor; // Establecer el color inicial
        }

        protected override void OnDetachingFrom(Button bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.Pressed -= OnPressed;
            bindable.Released -= OnReleased;
        }

        private void OnPressed(object sender, EventArgs e)
        {
            if (sender is Button button && button.IsEnabled)
            {
                button.BackgroundColor = HoverColor;
            }
        }

        private void OnReleased(object sender, EventArgs e)
        {
            if (sender is Button button && button.IsEnabled)
            {
                button.BackgroundColor = NormalColor;
            }
        }
    }
}
