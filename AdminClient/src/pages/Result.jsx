import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import Header from "../components/Header";
import LocationResult from "../components/LocationResult";
import PlocResult from "../components/PlocResult";
import ProviderResult from "../components/ProviderResult";
import SpecResult from "../components/SpecResult";
import { emptyPostState } from "../store/slices/PostSlice";
import { useFetcher } from "react-router-dom";

const Result = () => {
  const myData = {
    locationsVLD: ["string1", "string2"],
    locationsCTR: [],
    locationsINST: [],
    providersVLD: [],
    providersCTR: [],
    providersINST: [],
    providersLocationsVLD: [],
    providersLocationsCTR: [],
    providersLocationsINST: [],
    providersLocationsBINST: [],
    providersSPEC: [],
  };

  const dispatch = useDispatch();
  const [resultPage, setResultPage] = useState(1);

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

  const storedData = useSelector((state) => {
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
        body: storedData,
      }
    );


    const data = await res.json();
    // console.log(JSON.stringify(myData));
    // console.log(JSON.stringify(storedData));
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
            XXX-000-111
          </li>
          <li
            className={resultPage == 2 ? "active" : undefined}
            onClick={() => setResultPage(2)}
          >
            YYY-000-111
          </li>
          <li
            className={resultPage == 3 ? "active" : undefined}
            onClick={() => setResultPage(3)}
          >
            ZZZ-000-111
          </li>
          <li
            className={resultPage == 4 ? "active" : undefined}
            onClick={() => setResultPage(4)}
          >
            VVV-000-111
          </li>
        </ul>

        <div className="flex gap-5 items-center">
          <button onClick={clearAllChecks}>Clear</button>

          <button onClick={sendResult}>Run</button>
        </div>
      </div>

      {/* {resultData && resultPage == 1 && (
        <LocationResult resultData={resultData} />
      )}

      {resultData && resultPage == 2 && (
        <ProviderResult resultData={resultData} />
      )}

      {resultData && resultPage == 3 && <PlocResult resultData={resultData} />}

      {resultData && resultPage == 4 && <SpecResult resultData={resultData} />} */}

      {resultData && (
        <>
          {<LocationResult resultData={resultData} hidden={resultPage !== 1} />}
          <ProviderResult resultData={resultData} hidden={resultPage !== 2} />
          <PlocResult resultData={resultData} hidden={resultPage !== 3} />
          <SpecResult resultData={resultData} hidden={resultPage !== 4} />
        </>
      )}
    </div>
  );
};

export default Result;
