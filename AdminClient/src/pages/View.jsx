import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import Header from "../components/Header";
import LocationResult from "../components/LocationResult";
import PlocResult from "../components/PlocResult";
import ProviderResult from "../components/ProviderResult";
import SpecResult from "../components/SpecResult";
import { addOption, emptyPostState } from "../store/slices/PostSlice";
import useView from "../components/useView";
import { useLocation } from "react-router-dom";
import useClientData from "../components/useClientData";

const View = () => {
  const dispatch = useDispatch();
  const location = useLocation();
  const { data, fns } = useView();
  const clientName = useSelector((state) => state.data.client);
  const [resultPage, setResultPage] = useState(1);
  const {fns: clientDataFns} = useClientData();

  const [filterData, setFilterData] = useState("3");

  const clientResult = useSelector((state) => {
    return state.result;
  });
  const clientId = clientResult.clientId;
  const sessionId = clientResult.sessionId;

  const [viewData, setViewData] = useState(null);

  useEffect(() => {
    fns.getSessionResults(clientId, sessionId);
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
    const res = await fetch(
      `${import.meta.env.VITE_BASE_URL}/AdminApi/ProcessSelections`,
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(dataFromStore),
      }
    );
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
        <div className="grid grid-cols-3 max-[800px]:flex">
          <div className="text-sm">
            <span className="font-semibold">Client Id :</span> {clientId}
          </div>
          <div className="text-sm">
            <span className="font-semibold">Session Id :</span> {sessionId}
          </div>
        </div>
      </div>
      <div className="resultTabsCover grid grid-cols-3 m-4 mx-6 max-[800px]:flex">
        <ul className="resultTabs flex">
          <li
            className={
              "border-2 border-solid border-[var(--altRowBg)] rounded-md " +
              (resultPage == 1 ? "active" : undefined)
            }
            onClick={() => setResultPage(1)}
          >
            Locations
          </li>
          <li
            className={resultPage == 2 ? "active" : undefined}
            onClick={() => setResultPage(2)}
          >
            Providers
          </li>
        </ul>

        <div className="filterRadios flex items-center gap-9">
          <div className="flex items-center gap-2">
            <input
              id="A"
              value={1}
              onChange={(e) => {
                setFilterData(e.target.value);
                dispatch(addOption({ option: e.target.value }));
              }}
              type="radio"
              name="filters"
            />
            <label htmlFor="A">Connect</label>
          </div>
          <div className="flex items-center gap-2">
            <input
              id="B"
              value={2}
              onChange={(e) => {
                setFilterData(e.target.value);
                dispatch(addOption({ option: e.target.value }));
              }}
              type="radio"
              name="filters"
            />
            <label htmlFor="B">Importer</label>
          </div>
          <div className="flex items-center gap-2">
            <input
              id="C"
              value={3}
              onChange={(e) => {
                setFilterData(e.target.value);
                dispatch(addOption({ option: e.target.value }));
              }}
              type="radio"
              name="filters"
              defaultChecked
            />
            <label htmlFor="C">All</label>
          </div>
        </div>

        <div className="flex gap-3 items-center justify-self-end">
          <button
            onClick={() => clientDataFns.downloadHelp(clientId, sessionId)}
            className="bg-[var(--helpBg)] text-white"
          >
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
          <LocationResult
            filterData={filterData}
            resultData={viewData}
            hidden={resultPage !== 1}
          />
          <ProviderResult
            filterData={filterData}
            resultData={viewData}
            hidden={resultPage !== 2}
          />
          <PlocResult
            filterData={filterData}
            resultData={viewData}
            hidden={resultPage !== 3}
          />
          <SpecResult resultData={viewData} hidden={resultPage !== 4} />
        </>
      )}
    </div>
  );
};

export default View;
