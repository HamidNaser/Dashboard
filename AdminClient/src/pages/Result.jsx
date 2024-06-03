import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import Header from "../components/Header";
import LocationResult from "../components/LocationResult";
import PlocResult from "../components/PlocResult";
import ProviderResult from "../components/ProviderResult";
import SpecResult from "../components/SpecResult";
import { addOption, emptyPostState } from "../store/slices/PostSlice";

const Result = () => {
  const dispatch = useDispatch();
  const [resultPage, setResultPage] = useState(1);

  const [filterData, setFilterData] = useState("3");

  const clientResult = useSelector((state) => {
    return state.result;
  });
  const clientId = clientResult.clientId;
  const sessionId = clientResult.sessionId;

  const resultApi = `https://localhost:5001/AdminApi/GetSessionResults?clientId=${clientId}&sessionId=${sessionId}`;

  const [resultData, setResultData] = useState(null);
  const getResult = async () => {
    const res = await fetch(resultApi);

    // console.log(resultApi);

    const data = await res.json();

    setResultData(data);
  };
  useEffect(() => {
    getResult();
  }, []);

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
      "https://localhost:5001/AdminApi/ProcessSelections",
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

      <div className="resultTabsCover flex items-center justify-between m-4 mx-6">
        <ul className="resultTabs flex">
          <li
            className={resultPage == 1 ? "active" : undefined}
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
          <li
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
          </li>
        </ul>

        <div className="filterRadios flex items-center gap-9">
          <div className="flex items-center" >
            <label htmlFor="A">Connect</label>
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
          </div>
          <div className="flex items-center">
            <label htmlFor="B">Importer</label>
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
          </div>
          <div className="flex items-center">
            <label htmlFor="C">All</label>
            <input
              id="C"
              value={3}
              onChange={(e) => {setFilterData(e.target.value) ; dispatch(addOption({ option: e.target.value }));}}
              type="radio"
              name="filters"
              defaultChecked
            />
          </div>
        </div>

        <div className="flex gap-3 items-center">
          <button onClick={clearAllChecks}>Clear</button>

          <button onClick={sendResult}>Import</button>
        </div>
      </div>

      {resultData && (
        <>
          {
            <LocationResult
              filterData={filterData}
              resultData={resultData}
              hidden={resultPage !== 1}
            />
          }
          <ProviderResult
            filterData={filterData}
            resultData={resultData}
            hidden={resultPage !== 2}
          />
          <PlocResult
            filterData={filterData}
            resultData={resultData}
            hidden={resultPage !== 3}
          />
          <SpecResult resultData={resultData} hidden={resultPage !== 4} />
        </>
      )}
    </div>
  );
};

export default Result;
