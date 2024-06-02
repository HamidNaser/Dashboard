import React, { useState } from "react";
import { useDispatch } from "react-redux";
import {
  deleteAllPostData,
  removePostData,
  updateAllPostData,
  updatePostData,
} from "../store/slices/PostSlice";
import UserSvg from "../assets/UserSvg";
import HospitalSvg from "../assets/HospitalSvg";
UserSvg;
import ReactJson from "react-json-view";

const PlocResult = ({ resultData, hidden }) => {
  const keys = [
    "providersLocationsVLD",
    "providersLocationsCTR",
    "providersLocationsINST",
  ];

  const dispatch = useDispatch();

  const [showTable1Index, setShowTable1Index] = useState(-1);
  const [showTable2Index, setShowTable2Index] = useState(-1);
  const [showTable3Index, setShowTable3Index] = useState(-1);

  const toggleTable1 = (index) => {
    setShowTable1Index(showTable1Index === index ? -1 : index);
  };
  const toggleTable2 = (index) => {
    setShowTable2Index(showTable2Index === index ? -1 : index);
  };
  const toggleTable3 = (index) => {
    setShowTable3Index(showTable3Index === index ? -1 : index);
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
      <section className="ploc p-4 px-6">
        <header className="text-xl font-bold">
          {resultData.providersLocationsVLDHeader}
        </header>

        <div className="tableCover">
          <table className="mainTable">
            <thead>
              <tr>
                <th>Provider Id FG</th>
                <th>Message Type</th>
                <th>First Name</th>
                <th>Last Name</th>
                <th>Entity ID</th>
                <th>Location Name</th>
                <th>Error</th>
                <th>JSON</th>
                <th>Tracking ID</th>
                <th>
                  <input
                    onChange={(e) => checkAllBoxes([e.target.checked, keys[0]])}
                    type="checkbox"
                  />
                </th>
              </tr>
            </thead>

            {resultData.providersLocationsVLD.map((e, i) => {
              return (
                <React.Fragment key={i}>
                  <tbody className="tbody1">
                    <tr>
                      <td
                        onClick={() => toggleTable1(i)}
                        className={`groupId flex items-center ${
                          showTable1Index === i && "tableActive"
                        }`}
                      >
                        <UserSvg />
                        <svg
                          xmlns="http://www.w3.org/2000/svg"
                          className="h-4 fill-neutral-800 rotate-180 mr-2"
                          viewBox="0 0 512 512"
                        >
                          <path d="M98 190.06l139.78 163.12a24 24 0 0036.44 0L414 190.06c13.34-15.57 2.28-39.62-18.22-39.62h-279.6c-20.5 0-31.56 24.05-18.18 39.62z" />
                        </svg>

                        {e.npi}
                      </td>
                      <td>{e.messageType}</td>
                      <td>{e.firstName}</td>
                      <td>{e.lastName}</td>
                      <td>{e.marketEntityId}</td>
                      <td>{e.locationName}</td>
                      <td>
                        <button className="jsonBtn errorBtn bg-red-600 text-white rounded p-1 px-1.5 text-sm">
                          Error
                          <div className="jsonPopUp">
                            <ReactJson src={e.error} />
                          </div>
                        </button>
                      </td>
                      <td>
                        <button className="jsonBtn bg-green-600 text-white rounded p-1 px-1.5 text-sm">
                          JSON
                          <div className="jsonPopUp">
                            <ReactJson src={e.jsonValue} />
                          </div>
                        </button>
                      </td>
                      <td>{e.trackingId}</td>
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
                  </tbody>

                  {showTable1Index === i && (
                    <tbody className="tbody2">
                      <tr>
                        <td colSpan={11}>
                          <table className="insideTable">
                            <thead>
                              <tr>
                                <th>Location Name</th>
                                <th>Message Type</th>
                                <th>Address1</th>
                                <th>Client ID</th>
                                <th>Market Entity ID</th>
                                <th>Error</th>
                                <th>JSON</th>
                                <th>Tracking ID</th>
                              </tr>
                            </thead>
                            <tbody>
                              {e.providerLocationEntries.map((elem, index) => {
                                return (
                                  <tr key={index}>
                                    <td className="flex items-center">
                                      <HospitalSvg /> {elem.locationName}
                                    </td>
                                    <td>{elem.messageType}</td>
                                    <td>{elem.address1}</td>
                                    <td>{elem.clientId}</td>
                                    <td>{elem.marketEntityId}</td>
                                    <td>
                                      <button className="jsonBtn errorBtn bg-red-600 text-white rounded p-1 px-1.5 text-sm">
                                        Error
                                        <div className="jsonPopUp">
                                          <ReactJson src={elem.error} />
                                        </div>
                                      </button>
                                    </td>
                                    <td>
                                      <button className="jsonBtn bg-green-600 text-white rounded p-1 px-1.5 text-sm">
                                        JSON
                                        <div className="jsonPopUp">
                                          <ReactJson src={elem.jsonValue} />
                                        </div>
                                      </button>
                                    </td>
                                    <td>{elem.trackingId}</td>
                                  </tr>
                                );
                              })}
                            </tbody>
                          </table>
                        </td>
                      </tr>
                    </tbody>
                  )}
                </React.Fragment>
              );
            })}
          </table>
        </div>
      </section>

      <section className="ploc p-4 px-6">
        <header className="text-xl font-bold">
          {resultData.providersLocationsCTRHeader}
        </header>

        <div className="tableCover">
          <table className="mainTable">
            <thead>
              <tr>
                <th>Provider Id FG</th>
                <th>Message Type</th>
                <th>First Name</th>
                <th>Last Name</th>
                <th>Entity ID</th>
                <th>Location Name</th>
                <th>Error</th>
                <th>JSON</th>
                <th>Tracking ID</th>
                <th>
                  <input
                    onChange={(e) => checkAllBoxes([e.target.checked, keys[1]])}
                    type="checkbox"
                  />
                </th>
              </tr>
            </thead>

            {resultData.providersLocationsCTR.map((e, i) => {
              return (
                <React.Fragment key={i}>
                  <tbody key={i} className="tbody1">
                    <tr>
                      <td
                        onClick={() => toggleTable2(i)}
                        className={`groupId flex items-center ${
                          showTable2Index === i && "tableActive"
                        }`}
                      >
                        <UserSvg />

                        <svg
                          xmlns="http://www.w3.org/2000/svg"
                          className="h-4 fill-neutral-800 rotate-180 mr-2"
                          viewBox="0 0 512 512"
                        >
                          <path d="M98 190.06l139.78 163.12a24 24 0 0036.44 0L414 190.06c13.34-15.57 2.28-39.62-18.22-39.62h-279.6c-20.5 0-31.56 24.05-18.18 39.62z" />
                        </svg>

                        {e.npi}
                      </td>
                      <td>{e.messageType}</td>
                      <td>{e.firstName}</td>
                      <td>{e.lastName}</td>
                      <td>{e.marketEntityId}</td>
                      <td>{e.locationName}</td>
                      <td>
                        <button className="jsonBtn errorBtn bg-red-600 text-white rounded p-1 px-1.5 text-sm">
                          Error
                          <div className="jsonPopUp">
                            <ReactJson src={e.error} />
                          </div>
                        </button>
                      </td>
                      <td>
                        <button className="jsonBtn bg-green-600 text-white rounded p-1 px-1.5 text-sm">
                          JSON
                          <div className="jsonPopUp">
                            <ReactJson src={e.jsonValue} />
                          </div>
                        </button>
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
                  </tbody>

                  {showTable2Index === i && (
                    <tbody key={e} className="tbody2">
                      <tr>
                        <td colSpan={11}>
                          <table className="insideTable">
                            <thead>
                              <tr>
                                <th>Location Name</th>
                                <th>Message Type</th>
                                <th>Address1</th>
                                <th>Client ID</th>
                                <th>Market Entity ID</th>
                                <th>Error</th>
                                <th>JSON</th>
                                <th>Tracking ID</th>
                              </tr>
                            </thead>
                            <tbody>
                              {e.providerLocationEntries.map((elem, index) => {
                                return (
                                  <tr key={index}>
                                    <td className="flex items-center">
                                      <HospitalSvg /> {elem.locationName}
                                    </td>
                                    <td>{elem.messageType}</td>
                                    <td>{elem.address1}</td>
                                    <td>{elem.clientId}</td>
                                    <td>{elem.marketEntityId}</td>
                                    <td>
                                      <button className="jsonBtn errorBtn bg-red-600 text-white rounded p-1 px-1.5 text-sm">
                                        Error
                                        <div className="jsonPopUp">
                                          <ReactJson src={elem.error} />
                                        </div>
                                      </button>
                                    </td>
                                    <td>
                                      <button className="jsonBtn bg-green-600 text-white rounded p-1 px-1.5 text-sm">
                                        JSON
                                        <div className="jsonPopUp">
                                          <ReactJson src={elem.jsonValue} />
                                        </div>
                                      </button>
                                    </td>
                                    <td>{elem.trackingId}</td>
                                  </tr>
                                );
                              })}
                            </tbody>
                          </table>
                        </td>
                      </tr>
                    </tbody>
                  )}
                </React.Fragment>
              );
            })}
          </table>
        </div>
      </section>

      <section className="inst p-4 px-6">
        <header className="text-xl font-bold">
          {resultData.providersLocationsBINSTHeader}
        </header>

        <div className="flex items-center justify-between px-6 font-semibold text-neutral-600">
          <span>Group Id</span>

          <input
            onChange={(e) => checkAllBoxes([e.target.checked, keys[2]])}
            type="checkbox"
          />
        </div>

        {resultData.providersLocationsBINST.map((e, i) => {
          return (
            <div className="groupIdCover" key={i}>
              <div
                className={`groupId px-6 flex items-center justify-between ${
                  showTable3Index === i && "tableActive"
                }`}
              >
                <div
                  onClick={() => toggleTable3(i)}
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
                  <div className="ml-3 flex items-center gap-3">
                    <button className="jsonBtn errorBtn bg-red-600 text-white rounded p-1 px-1.5 text-sm">
                      Error
                      <div className="jsonPopUp">
                        <ReactJson src={e.error} />
                      </div>
                    </button>
                    <button className="jsonBtn bg-green-600 text-white rounded p-1 px-1.5 text-sm">
                      JSON
                      <div className="jsonPopUp">
                        <ReactJson src={e.jsonValue} />
                      </div>
                    </button>
                  </div>
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

              {showTable3Index === i && (
                <div className="tableCover">
                  <table>
                    <thead>
                      <tr>
                        <th>Location Name</th>
                        <th>Message Type</th>
                        <th>Address 1</th>
                        <th>Entity Id</th>
                        <th>Error</th>
                        <th>JSON</th>
                        <th>Client Id</th>
                      </tr>
                    </thead>

                    <tbody>
                      {e.locations.map((e, i) => {
                        return (
                          <tr key={i}>
                            <td className="flex items-center">
                              <HospitalSvg /> {e.locationName}
                            </td>
                            <td>{e.messageType}</td>
                            <td>{e.address1}</td>
                            <td>{e.marketEntityId}</td>
                            <td>
                              <button className="jsonBtn errorBtn bg-red-600 text-white rounded p-1 px-1.5 text-sm">
                                Error
                                <div className="jsonPopUp">
                                  <ReactJson src={e.error} />
                                </div>
                              </button>
                            </td>
                            <td>
                              <button className="jsonBtn bg-green-600 text-white rounded p-1 px-1.5 text-sm">
                                JSON
                                <div className="jsonPopUp">
                                  <ReactJson src={e.jsonValue} />
                                </div>
                              </button>
                            </td>
                            <td>{e.clientId}</td>
                          </tr>
                        );
                      })}
                    </tbody>
                  </table>
                </div>
              )}
            </div>
          );
        })}
      </section>
    </div>
  );
};

export default PlocResult;
