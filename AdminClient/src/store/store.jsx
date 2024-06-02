import { configureStore } from "@reduxjs/toolkit";
import MenuSlice from "./slices/MenuSlice";
import PostSlice from "./slices/PostSlice";
import ResultSlice from "./slices/ResultSlice";
const store = configureStore({
  reducer: {
    menu: MenuSlice,
    result: ResultSlice,
    postData: PostSlice,
  },
});

export default store;
