using TMPro;
using Zenject;

namespace Q17pD.Brachistochrone.Items
{
    public interface IObvservable
    {
        public string Name { get; }
        public Outline Outline { get; }
        public void ShowName(TextMeshProUGUI _nameText)
        {
            _nameText.text = Name;
            Outline.enabled = true;
        }
        public void HideName(TextMeshProUGUI _nameText)
        {
            _nameText.text = string.Empty;
            Outline.enabled = false;
        }
    }
}
