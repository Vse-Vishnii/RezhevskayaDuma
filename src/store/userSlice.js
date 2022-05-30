import { createSlice } from "@reduxjs/toolkit";

const userSlice = createSlice({
  name: "user",
  initialState: { user: null, isUserLoadedPage: false },
  reducers: {
    setUser(state, action) {
      state.user = action.payload;
    },
    isUserLoadedPage(state) {
      state.isUserLoadedPage = true;
    },
    clearUser(state) {
      state.user = null;
    },
  },
});

export const { setUser, clearUser, isUserLoadedPage } = userSlice.actions;

export default userSlice.reducer;
