import React, { useState } from "react";
import { useDispatch } from "react-redux";
import {
  deleteAllPostData,
  removePostData,
  updateAllPostData,
  updatePostData,
} from "../store/slices/PostSlice";
import ReactJson from "react-json-view";
import HospitalSvg from "../assets/HospitalSvg";

const LocationResult = ({ filterData, resultData, hidden }) => {
  // const keys = ["locationsVLD", "locationsCTR", "locationsINST"];
  const keys = ["locationsVLD", "locationsCTR", "locationsINST"];

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
      {filterData != 1 && (
        <>
        <section className="p-4 px-6">
          <header className="text-xl font-bold">
            {resultData.locationsVLDHeader}
          </header>

          <div className="tableCover">
            <table>
              <thead>
                <tr>
                  <th>Location Name</th>
                  <th>Message Type</th>
                  <th>Address</th>
                  <th>Client ID</th>
                  <th>Entity ID</th>
                  {/* <th>Error</th> */}
                  <th>Tracking ID</th>
                  <th>JSON</th>
                  <th>
                    <input
                      onChange={(e) =>
                        checkAllBoxes([e.target.checked, keys[0]])
                      }
                      type="checkbox"
                    />
                  </th>
                </tr>
              </thead>

              <tbody>
                {resultData.locationsVLD.map((e, i) => {
                  return (
                    <tr key={i}>
                      <td className="flex items-center">
                        <HospitalSvg /> {e.locationName}
                      </td>
                      <td>{e.messageType}</td>
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
                        <button className="jsonBtn bg-green-600 text-white rounded p-1 px-1.5 text-sm">
                          JSON
                        </button>
                        <div className="jsonPopUp">
                          <ReactJson src={e.jsonValue} />
                        </div>
                      </td>
                      <td>
                        <input
                          onChange={(event) =>
                            event.target.checked
                              ? storeData(i, keys[0])
                              : removeData(i, keys[0])
                          }
                          type="checkbox"
                          className={keys[0]}
                        />
                      </td>
                    </tr>
                  );
                })}
              </tbody>
            </table>
          </div>
        </section>
        <section className="p-4 px-6">
          <header className="text-xl font-bold">
            {resultData.locationsCTRHeader}
          </header>
          <div className="tableCover">
            <table>
              <thead>
                <tr>
                  <th>Location Name</th>
                  <th>Message Type</th>
                  <th>Address</th>
                  <th>Client ID</th>
                  <th>Entity ID</th>
                  <th>Error</th>
                  <th>JSON</th>
                  <th>Tracking ID</th>
                  <th>
                    <input
                      type="checkbox"
                      onChange={(e) =>
                        checkAllBoxes([e.target.checked, keys[1]])
                      }
                    />
                  </th>
                </tr>
              </thead>

              <tbody>
                {resultData.locationsCTR.map((e, i) => {
                  return (
                    <tr key={i}>
                      <td className="flex items-center">
                        <HospitalSvg /> {e.locationName}
                      </td>
                      <td>{e.messageType}</td>
                      <td>{e.address1}</td>
                      <td>{e.clientId}</td>
                      <td>{e.marketEntityId}</td>
                      <td className="relative">
                        <button className="jsonBtn errorBtn bg-red-600 text-white rounded p-1 px-1.5 text-sm">
                          Error
                        </button>
                        <div className="jsonPopUp">
                          <ReactJson src={e.error} />
                        </div>
                      </td>
                      <td className="relative">
                        <button className="jsonBtn bg-green-600 text-white rounded p-1 px-1.5 text-sm">
                          JSON
                        </button>
                        <div className="jsonPopUp">
                          <ReactJson src={e.jsonValue} />
                        </div>
                      </td>
                      <td>{e.trackingId}</td>
                      <td>
                        <input
                          onChange={(event) =>
                            event.target.checked
                              ? storeData(i, keys[1])
                              : removeData(i, keys[1])
                          }
                          type="checkbox"
                          className={keys[1]}
                        />
                      </td>
                    </tr>
                  );
                })}
              </tbody>
            </table>
          </div>
        </section>
        </>
      )}

      {filterData != 2 && (
        <section className="inst p-4 px-6">
          <header className="text-xl font-bold">
            {resultData.locationsINSTHeader}
          </header>
          <div className="flex items-center justify-between px-6 font-semibold text-neutral-600">
            <span>Group Id</span>
            <div>
              <span className="mr-48 text-sm">Error</span>
              <input
                onChange={(e) => checkAllBoxes([e.target.checked, keys[2]])}
                type="checkbox"
              />
            </div>
          </div>
          {resultData?.locationsINST?.map((e, i) => {
            return (
              <div key={i} className="tableCover">
                <div className="groupIdCover" key={i}>
                  <div
                    className={`groupId px-6 flex items-center justify-between ${
                      showTableIndex === i && "tableActive"
                    }`}
                  >
                    <div
                      onClick={() => toggleTable(i)}
                      className="flex items-center py-4 gap-2 grow"
                    >
                      <svg
                        xmlns="http://www.w3.org/2000/svg"
                        className="h-4 fill-neutral-800 rotate-180"
                        viewBox="0 0 512 512"
                      >
                        <path d="M98 190.06l139.78 163.12a24 24 0 0036.44 0L414 190.06c13.34-15.57 2.28-39.62-18.22-39.62h-279.6c-20.5 0-31.56 24.05-18.18 39.62z" />
                      </svg>

                      {e.groupId}
                    </div>

                    <input
                      onChange={(event) =>
                        event.target.checked
                          ? storeData(i, keys[2])
                          : removeData(i, keys[2])
                      }
                      type="checkbox"
                      className={keys[2]}
                    />
                  </div>

                  {showTableIndex === i && (
                    <div className="tableCover">
                      <table>
                        <thead>
                          <tr>
                            <th>Location Name</th>
                            <th>Message Type</th>
                            <th>Address</th>
                            <th>Client ID</th>
                            <th>Entity ID</th>
                            <th>Error</th>
                            <th>JSON</th>
                            <th>Tracking ID</th>
                          </tr>
                        </thead>

                        <tbody>
                          {e.locations.map((elem, index) => {
                            return (
                              <tr key={index}>
                                <td className="flex items-center">
                                  <HospitalSvg /> {elem.locationName}
                                </td>
                                <td>{elem.messageType}</td>
                                <td>{elem.address1}</td>
                                <td>{elem.clientId}</td>
                                <td>{elem.marketEntityId}</td>
                                <td className="relative">
                                  <button className="jsonBtn errorBtn bg-red-600 text-white rounded p-1 px-1.5 text-sm">
                                    Error
                                  </button>
                                  <div className="jsonPopUp">
                                    <ReactJson src={e.error} />
                                  </div>
                                </td>

                                <td className="relative">
                                  <button className="jsonBtn bg-green-600 text-white rounded p-1 px-1.5 text-sm">
                                    JSON
                                  </button>
                                  <div className="jsonPopUp">
                                    <ReactJson src={e.jsonValue} />
                                  </div>
                                </td>
                                <td>{elem.trackingId}</td>
                              </tr>
                            );
                          })}
                        </tbody>
                      </table>
                    </div>
                  )}
                </div>
              </div>
            );
          })}
        </section>
      )}
    </div>
  );
};

export default LocationResult;
