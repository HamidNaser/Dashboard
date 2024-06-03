import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import Header from "../components/Header";
import { addOption, emptyPostState } from "../store/slices/PostSlice";
import useView from "../components/useView";
import LocationInput from "../components/ViewInput/LocationInput";
import ProviderInput from "../components/ViewInput/ProviderInput";
import useClientData from "../components/useClientData";

const ViewInput = () => {
  const dispatch = useDispatch();
  const { data, fns } = useView();
  const {fns: clientDataFns} = useClientData();
  const clientName = useSelector((state) => state.data.client);
  const [resultPage, setResultPage] = useState(1);

  const [filterData, setFilterData] = useState("3");

  const clientResult = useSelector((state) => {
    return state.result;
  });
  const clientId = clientResult.clientId;
  const sessionId = clientResult.sessionId;

  const [viewData, setViewData] = useState(null);

  useEffect(() => {
    fns.getSessionInput(clientId, sessionId);
  }, []);

  useEffect(() => {
    if (data.viewResults) {
      setViewData(data.viewResults);
    }
  }, [data.viewResults]);

  const clearAllChecks = () => {
    const checkBoxes = document.querySelectorAll('input[type="checkbox"]');

    checkBoxes.forEach((e) => {
      e.checked = false;
    });

    dispatch(emptyPostState());
  };

  // data from store
  const dataFromStore = useSelector((state) => {
    return state.postData;
  });

  const sendResult = async (e) => {
    e.preventDefault();
    const res = await fetch(`${import.meta.env.VITE_BASE_URL}/AdminApi/ProcessSelections`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(dataFromStore),
    });
    const data = await res.json();

    // data that i am sending
    console.log(dataFromStore);

    // response i am getting from server
    console.log(data);
  };

  return (
    <div className="resultPage page grow">
      <Header />
      <div className="px-5 w-full">
        <div className="name font-bold text-xl my-3">{clientName}</div>
        <div className="grid grid-cols-2">
          <div className="text-sm">
            <span className="font-semibold">Client Id :</span> {clientId}
          </div>
          <div className="text-sm">
            <span className="font-semibold">Session Id :</span> {sessionId}
          </div>
        </div>
      </div>
      <div className="resultTabsCover flex items-center justify-between m-4 mx-6">
        <ul className="resultTabs flex">
          <li className={"border-2 border-solid border-[var(--altRowBg)] rounded-md " + (resultPage == 1 ? "active" : undefined)} onClick={() => setResultPage(1)}>
            Locations
          </li>
          <li className={resultPage == 2 ? "active" : undefined} onClick={() => setResultPage(2)}>
            Providers
          </li>
          {/* <li
            className={resultPage == 3 ? "active" : undefined}
            onClick={() => setResultPage(3)}
          >
            Providers.Locations
          </li>
          <li
            className={resultPage == 4 ? "active" : undefined}
            onClick={() => setResultPage(4)}
          >
            Providers Specialties
          </li> */}
        </ul>

        <div className="flex gap-3 items-center">
          <button onClick={() => clientDataFns.downloadHelp(clientId, sessionId)} className="bg-[var(--helpBg)] text-white">
            Help
          </button>

          <button onClick={clearAllChecks} className="bg-black text-white">
            Clear
          </button>

          <button onClick={sendResult} className="bg-black text-white">
            Import
          </button>
        </div>
      </div>

      {viewData && (
        <>
          <LocationInput filterData={filterData} resultData={viewData} hidden={resultPage !== 1} />
          <ProviderInput filterData={filterData} resultData={viewData} hidden={resultPage !== 2} />
        </>
      )}
    </div>
  );
};

export default ViewInput;
