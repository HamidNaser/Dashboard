import { configureStore } from "@reduxjs/toolkit";
import MenuSlice from "./slices/MenuSlice";
import PostSlice from "./slices/PostSlice";
import ResultSlice from "./slices/ResultSlice";
import DataSlice from "./slices/DataSlice";
const store = configureStore({
  reducer: {
    menu: MenuSlice,
    result: ResultSlice,
    postData: PostSlice,
    data: DataSlice,
  },
});

export default store;
