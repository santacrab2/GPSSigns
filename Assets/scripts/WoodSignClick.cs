using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
public class WoodSignClick : MonoBehaviour
{
    public string moreInfoText;
    public GameObject MainSign;
    public GameObject infoPanel;
    bool isPanelActive = false;
    bool spawned = false;
    GameObject newPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    public void OnTouch(InputValue value)
    {
        Vector2 touchposition = value.Get<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(touchposition);
        Physics.Raycast(ray, out var hitinfo);
        if (hitinfo.transform != null && hitinfo.transform.gameObject.name == "WoodSignWithText")
        {
            OnSignClick();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSignClick()
    {
        Vector3 spawnposition = new Vector3(MainSign.transform.position.x, MainSign.transform.position.y - 2, MainSign.transform.position.z);
        if (!spawned)
            newPanel = Instantiate(infoPanel, spawnposition, Quaternion.identity);
        if (newPanel == null) return;
        var panelText = newPanel.GetNamedChild("Scroll View").GetNamedChild("Viewport").GetNamedChild("Content").GetComponentInChildren<TextMeshProUGUI>();
        panelText.text = MoreInfoTexts[moreInfoText];
        spawned = true;
        isPanelActive = !isPanelActive;
        newPanel.SetActive(isPanelActive);
    }
    private Dictionary<string, string> MoreInfoTexts = new()
    {
        {"School House", "This one-room school is the oldest remaining structure in the village. It was established to serve the children of the lumber community and survived to serve the children of the cranberry workers. The school closed after the 1914-1915 school year and the Double Trouble students were bused to the Berkeley Township School in Bayville for their education. In later years, the schoolhouse building was used to house seasonal workers." },
        {"Burke House", "This building was home to the Burke family from 1938 until 1957. David T. Burke was foreman of the cranberry processing operations until 1967 when his son George took over. George was the youngest of 13 children and spent most of his life at Double Trouble." },
        {"Cranberry Sorting and Packing House", "This building was the center of the cranberry harvest operations. Here, hand-scooped cranberries were sorted according to size and quality and then packed for market. It is one of the last remaining dry-harvest era screen houses in the state and was restored through a New Jersey Historic Trust grant in the 1990s. It is now a restored building with artifacts and exhibits interpreting the former cranberry culture at Double Trouble Village." },
        {"General Store", "The general store provided the early villagers with staples such as oatmeal, flour, pork and sugar. In February 1910, fresh pork was 20 cents a pound, ham was 18 cents a pound, canned salmon and sardines sold for 15 cents a can, oatmeal was 5 cents a pound, oranges were 30 cents a dozen, and pickles were a penny each. By the 1940s, the store was only open seasonally during the harvest so that the workers could purchase incidental items like tobacco and candy. " },
        {"Garage & Workshop", "The oldest part of this structure housed the blacksmith and repair shop for the sawmill and cranberry operations. The eastern portion of the shop was added after 1941. Today, this building houses park operations and the maintenance shop. " },
        {"Showers", "The shower room was a shared facility, with separate men’s and women’s rooms. Each room had two shower stalls. The women’s side also had a laundry sink." },
        {"Bunk House", "This communal house is where single workers lived during the cranberry harvest. It was modestly furnished with bunk beds and some shelves. This is one of the oldest buildings in the village and dates to the lumber industry. A metal-clad cook shed once stood in front of the bunk house. " },
        {"Picker Cottage", "This pickers’ cottage housed families during the cranberry harvest. It served as the park caretaker’s residence in the 1970s to the 1990s. George Burke lived here with his wife. Newspapers often ran stories on them with the headline, “Double Trouble. Population: 2.” " },
        {"Jumper Building", "Wet cranberries were dried and sorted in this barn-like structure. It was also used for storing farm equipment." },
        {"Saw Mill", "The sawmill produced lumber, clapboard, shingles, lath and other products both for sale and use in the village and cranberry operations. This sawmill was significantly damaged by a fire and rebuilt in 1904. The metal roof and walls were added for protection from frequent forest fires. The saws were driven by belts connected to a pulley in the basement. The drive shaft was converted from water powered to steam powered and eventually a Witte Engine. As the Double Trouble Company focused more on cranberry production, the sawmill was run less. The sawmill had serious structural issues and was restored through a New Jersey Historic Trust grant in the 1990s. It is now a restored building with artifacts and exhibits interpreting the former sawmill industry at Double Trouble Village. " },
        {"Harvest Foremans House", "This cabin was the seasonal home of the migrant workers’ foreman, Alfio Musumeci. Early every morning during picking season, Musumeci would run multiple parallel string lines through the bogs, about ten feet apart and anchored by wooden stakes. Pickers were assigned to specific lanes and told to “Pick clean! Pick clean!” When the cranberry harvest was completed, he and the pickers returned to Pennsylvania. Musumeci worked at Double Trouble for 57 harvest seasons. " },
        {"Pickers Cottage", "These pickers’ cottages each housed two families during the harvest season. Every year 30-40 migrant workers arrived on Labor Day weekend and stayed until Thanksgiving. They worked exclusively in the bogs handpicking cranberries. They started picking about nine o’clock, after the sun had burned off the morning dew, and worked until late afternoon. Workers were given a ticket for each box of cranberries picked. At the end of the week the tickets were exchanged for cash at the General Store. The cottage closest to the parking area is now an information center with restrooms." },
        {"Pickers Cottage & Public Restrooms", "These pickers’ cottages each housed two families during the harvest season. Every year 30-40 migrant workers arrived on Labor Day weekend and stayed until Thanksgiving. They worked exclusively in the bogs handpicking cranberries. They started picking about nine o’clock, after the sun had burned off the morning dew, and worked until late afternoon. Workers were given a ticket for each box of cranberries picked. At the end of the week the tickets were exchanged for cash at the General Store. The cottage closest to the parking area is now an information center with restrooms." },
        {"Company Foremans House", "This building was the year-round home of the “head” foreman who oversaw the management of the cranberry and sawmill operations." }
    };
}
