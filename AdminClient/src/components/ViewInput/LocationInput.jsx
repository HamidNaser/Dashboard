import React, { useState } from "react";
import { useDispatch } from "react-redux";
import HospitalSvg from "../../assets/HospitalSvg";
import ReactJson from "react-json-view";

const LocationInput = ({ filterData, resultData, hidden }) => {
  // const keys = ["locationsVLD", "locationsCTR", "locationsINST"];
  const keys = ["locations"];

  const dispatch = useDispatch();
  const [showTableIndex, setShowTableIndex] = useState(-1);

  const toggleTable = (index) => {
    setShowTableIndex(showTableIndex === index ? -1 : index);
  };

  const storeData = (i, key) => {
    dispatch(updatePostData([resultData, i, key]));
  };

  const removeData = (i, key) => {
    dispatch(removePostData([resultData, i, key]));
  };

  const updateAllData = (key) => {
    dispatch(updateAllPostData([resultData, key]));
  };

  const removeAllData = (key) => {
    dispatch(deleteAllPostData(key));
  };

  const checkAllBoxes = ([isChecked, key]) => {
    const checkBoxes = document.querySelectorAll(`.${key}`);
    if (isChecked === true) {
      checkBoxes.forEach((e) => {
        e.checked = true;
      });
      updateAllData(key);
    } else {
      checkBoxes.forEach((e) => {
        e.checked = false;
      });
      removeAllData(key);
    }
  };

  return (
    <div hidden={hidden}>
      <section className="p-4 px-6">
        {/* <header className="text-xl font-bold">{resultData.locations}</header> */}

        <div className="tableCover">
          <table>
            <thead>
              <tr>
                <th>Location Name</th>
                <th>Address</th>
                <th>Client ID</th>
                <th>Entity ID</th>
                {/* <th>Error</th> */}
                <th>Tracking ID</th>
                <th>JSON</th>
                <th>
                  <input onChange={(e) => checkAllBoxes([e.target.checked, keys[0]])} type="checkbox" />
                </th>
              </tr>
            </thead>
            <tbody>
              {resultData.locations.map((e, i) => {
                return (
                  <tr key={i}>
                    <td className="flex items-center">
                      <HospitalSvg /> {e.locationName}
                    </td>
                    <td>{e.address1}</td>
                    <td>{e.clientId}</td>
                    <td>{e.marketEntityId}</td>
                    {/* <td className="relative">
                          <button className="jsonBtn errorBtn bg-red-600 text-white rounded p-1 px-1.5 text-sm">
                            Error
                          </button>
                          <div className="jsonPopUp">
                            <ReactJson src={e.error} />
                          </div>
                        </td> */}
                    <td>{e.trackingId}</td>
                    <td className="relative">
                      <button className="jsonBtn bg-green-600 text-white rounded p-1 px-1.5 text-sm">JSON</button>
                      <div className="jsonPopUp">
                        <ReactJson src={e.jsonValue} />
                      </div>
                    </td>
                    <td>
                      <input onChange={(event) => (event.target.checked ? storeData(i, keys[0]) : removeData(i, keys[0]))} type="checkbox" className={keys[0]} />
                    </td>
                  </tr>
                );
              })}
            </tbody>
          </table>
        </div>
      </section>
    </div>
  );
};

export default LocationInput;
