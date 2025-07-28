
using UnityEngine;

public class weapeonSwitching : MonoBehaviour
{
    public int selectedWeapeon = 0;
    // Start is called before the first frame update
    void Start()
    {
        SelectWeapeon();
    }

    // Update is called once per frame
    void Update()
    {
        int previousSelectedWeapeon = selectedWeapeon;

        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if(selectedWeapeon >= transform.childCount - 1)
            {
                selectedWeapeon = 0;
            }
            else selectedWeapeon++;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapeon <= 0)
            {
                selectedWeapeon = transform.childCount - 1; ;
            }
            else selectedWeapeon--;
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapeon = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedWeapeon = 1;
        }

        if (previousSelectedWeapeon != selectedWeapeon)
        {
            SelectWeapeon();
        }
    }

    void SelectWeapeon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if (i == selectedWeapeon)
            {
                weapon.gameObject.SetActive(true);
            }
            else weapon.gameObject.SetActive(false);
            i++;
        }
    }
}
