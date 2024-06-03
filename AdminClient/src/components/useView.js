import axios from "axios";
import { useState } from "react";

export default function useView() {
  let data = {};

  const [sessionLoading, setSessionLoading] = useState(false);
  data.sessionLoading = sessionLoading;

  const [viewResults, setViewResults] = useState(null);
  data.viewResults = viewResults;
  
  const getSessionResults = async (clientId, sessionId) => {
    setSessionLoading(true);
    try {
      const api = import.meta.env.VITE_BASE_URL + `/AdminApi/GetSessionResults?clientId=${clientId}&sessionId=${sessionId}`;

      const res = await axios.get(api);
      setViewResults(res.data);
    } catch (err) {
      console.log("error :", err);
    } finally {
      setSessionLoading(false);
    }
  };

  const getSessionInput = async (clientId, sessionId) => {
    setSessionLoading(true);
    try {
      const api = import.meta.env.VITE_BASE_URL + `/AdminApi/GetSessionInput?clientId=${clientId}&sessionId=${sessionId}`;

      const res = await axios.get(api);
      setViewResults(res.data);
    } catch (err) {
      console.log("error :", err);
    } finally {
      setSessionLoading(false);
    }
  };

  let fns = {
    getSessionResults,
    getSessionInput
  };
  return { data, fns };
}
