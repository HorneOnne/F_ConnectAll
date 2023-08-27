using UnityEngine;

namespace ConnectAll
{
    public class InputHandler : MonoBehaviour
    {
        public static InputHandler Instance {get; private set;}
        public bool IsDragging { get; set; }

        [field: SerializeField] public Chip Chip{get; set;}
        [field: SerializeField] public GridSlot LastestGridSlotEnter { get;  set; }

        private void Awake()
        {
            Instance = this;
        }


        private void Update()
        {
            if(IsDragging)
            {
                if (Chip != null)
                {
                    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Chip.transform.position = mousePosition;
                }
            }       
        }


        public void SetChip(Chip chip)
        {
            if(Chip == null)
            {
                Chip = chip;
            }
        }

        public void ResetChip()
        {
            Chip = null;
        }


        public void SetLastestGridSlotEnter(GridSlot gridSlot)
        {
            LastestGridSlotEnter = gridSlot;
        }


        public void UpdateChipPosition()
        {
            Chip.transform.position = LastestGridSlotEnter.transform.position;
            LastestGridSlotEnter.SetChip(Chip);

            Chip = null;
            LastestGridSlotEnter = null;
        }

    }
}