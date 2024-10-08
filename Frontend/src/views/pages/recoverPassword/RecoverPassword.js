import React from "react"
import { Link } from 'react-router-dom'
import { CCard, CContainer, CForm, CFormInput, CInputGroup, CInputGroupText, CRow, CCol, CButton } from "@coreui/react"
import handleRecoverPasswordSubmit from "./handleRecoverPasswordSubmit"

const RecoverPassword = () => {
    return (
    <div className="bg-body-tertiary min-vh-100 d-flex flex-row align-items-center">
        <CContainer>
            <CRow className="justify-content-center">
                <CCol md={9} lg={7} xl={6}>
                    <CCard className="p-4">
                        <CForm onSubmit={handleRecoverPasswordSubmit}>
                            <h1>Recover password</h1>
                            <p className="text-body-secondary mb-0  ">Introduce your email in the box below</p>
                            <p className="text-body-secondary mt-0">If the email exists we will send you a new password</p>
                            <CInputGroup className="mb-3">
                                <CInputGroupText>@</CInputGroupText>
                                <CFormInput placeholder="Email" name='email' autoComplete="email" />
                            </CInputGroup>
                            <CRow>
                                    <CButton type='submit' color="primary" className="px-4">
                                    Send
                                    </CButton>
                            </CRow>
                        </CForm>
                    </CCard>
                </CCol>
            </CRow>
        </CContainer>
    </div>
    )
}

export default RecoverPassword